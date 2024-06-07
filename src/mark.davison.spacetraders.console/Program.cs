using mark.davison.spacetraders.console.Procedures;
using mark.davison.spacetraders.core.Utility;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace mark.davison.spacetraders.console;

internal class Program
{
    static void PrintObject<T>(string title, T? obj)
    {
        if (!string.IsNullOrEmpty(title))
        {
            Console.WriteLine("====={0}=====", title);
        }
        if (obj == null)
        {
            return;
        }
        Console.WriteLine(JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        }));
    }



    static async Task Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SpacetradersDbContext>();
        optionsBuilder.UseSqlite("Data Source=C:/temp/spacetraders.db");
        var dbContext = new SpacetradersDbContext(optionsBuilder.Options);

        //await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        // TODO: Migrate vs ensurecreated

        var agent = await SyncAgent(dbContext);

        var api = new SpaceTradersApiClient(new AuthenticationContext { Token = agent.Token }, new HttpClient());

        { // TEMP: Ensure initial mining ship is present/purchased
            var shipInfo = await QueryShips.QueryShipInfo(dbContext, api, false);

            PrintObject("SHIP INFO", shipInfo);

            if (!shipInfo.Any(_ => _.Role == ShipRole.EXCAVATOR))
            {
                Console.WriteLine("We need to purchase a mining ship");
                var hqSystem = agent.Headquarters.GetSystemFromWaypoint();

                var purchaseResult = await BuyShip.PurchaseShipByTypeFromSystem(
                    dbContext,
                    api,
                    ShipType.SHIP_MINING_DRONE,
                    hqSystem);

                if (string.IsNullOrEmpty(purchaseResult.Symbol))
                {
                    return;
                }

                PrintObject("PURCHASED MINING DRONE", purchaseResult);
            }
        }

        var ships = await QueryShips.QueryShipInfo(dbContext, api, false);
        foreach (var ship in ships)
        {
            if (ship.Role != ShipRole.EXCAVATOR)
            {
                Console.WriteLine("Cannot handle ship of role '{0}' yet", ship.Role);
                continue;
            }

            if (ship.ContractId == null)
            {
                Console.WriteLine("{0} ship {1} does not have a contract", ship.Role, ship.Symbol);
                await QueryContract.AssignAvailableContract(dbContext, api, ship.Id, ship.Role);
            }
            else
            {
                Console.WriteLine("{0} ship {1} has a contract", ship.Role, ship.Symbol);
                await PerformContract.UpdateContract(dbContext, api, ship.Id, ship.ContractId.Value);
            }
        }


        //    var asteroidWaypoints = await api.GetSystemWaypointsAsync(
        //        null,
        //        null,
        //        WaypointType.ENGINEERED_ASTEROID,
        //        null,
        //        $"{hqInfo[0]}-{hqInfo[1]}");
        //    PrintObject("ASTEROIDS", asteroidWaypoints);
        //    await Task.Delay(1000);


        //    var targetAsteroid = asteroidWaypoints.Data.FirstOrDefault();
        //    if (targetAsteroid == null)
        //    {
        //        Console.Error.WriteLine("No asteroid to go to");
        //        return;
        //    }

        //    if (miningShip.Nav.WaypointSymbol != targetAsteroid.Symbol)
        //    {

        //        var orbitShip = await api.OrbitShipAsync(miningShip.Symbol);
        //        PrintObject("ORBIT_SHIP", orbitShip);
        //        await Task.Delay(1000);

        //        if (orbitShip.Data.Nav.Status != ShipNavStatus.IN_ORBIT)
        //        {
        //            Console.Error.WriteLine("Ship is not in orbit, gotta wait...");
        //            return;
        //        }

        //        var navigateToAsteroid = await api.NavigateShipAsync(new()
        //        {
        //            WaypointSymbol = targetAsteroid.Symbol
        //        }, miningShip.Symbol);
        //        PrintObject("NAVIGATE TO ASTEROID", navigateToAsteroid);
        //        await Task.Delay(1000);
        //    }

        //    PrintObject("MINING SHIP NAV", miningShip.Nav);
        //    if (miningShip.Nav.Status == ShipNavStatus.IN_TRANSIT)
        //    {
        //        Console.Error.WriteLine("Ship is in transit...");
        //        return;
        //    }

        //    if (miningShip.Nav.Status == ShipNavStatus.IN_ORBIT)
        //    {
        //        if (miningShip.Fuel.Current < miningShip.Fuel.Capacity)
        //        {
        //            var refuelShip = await api.RefuelShipAsync(new()
        //            {
        //                Units = miningShip.Fuel.Capacity - miningShip.Fuel.Current
        //            }, miningShip.Symbol);
        //            PrintObject("REFUEL", refuelShip);
        //            await Task.Delay(1000);
        //            var dockShip = await api.DockShipAsync(miningShip.Symbol);
        //            PrintObject("DOCK TO ASTEROID", dockShip);
        //            await Task.Delay(1000);
        //        }
        //    }

        //    // ORBIT then extract

        //    var extract = await api.ExtractResourcesAsync(new(), miningShip.Symbol);
        //    PrintObject("EXTRACT", extract);
        //    await Task.Delay(1000);

        //    // Refuel

        //    Console.WriteLine("Completed...");
        //    Console.ReadLine();
        //}
        Console.WriteLine("Completed...");
        Console.ReadLine();
    }


    private static async Task SyncShips(SpacetradersDbContext dbContext, SpaceTradersApiClient api)
    {
        // TODO: Move inside to where we create new agent
        var ships = await api.GetMyShipsAsync(null, null);
        await Task.Delay(1000);

        bool changeMade = false;

        var existingShips = await dbContext.Set<Spaceship>().ToListAsync();
        foreach (var ship in ships.Data)
        {
            var existingShip = existingShips.FirstOrDefault(_ => _.Symbol == ship.Symbol);

            if (existingShip == null)
            {
                changeMade = true;
                await dbContext.AddAsync(new Spaceship
                {
                    Id = Guid.NewGuid(),
                    GlobalState = SpaceshipState.IDLE,
                    Symbol = ship.Symbol
                });
            }
            else
            {
                if (ship.Registration.Role != existingShip.ShipRole)
                {
                    changeMade = true;
                    existingShip.ShipRole = ship.Registration.Role;
                    dbContext.Update(existingShip);
                }
            }
        }

        if (changeMade)
        {
            await dbContext.SaveChangesAsync();
        }
    }

    private static async Task<SpaceAgent> SyncAgent(SpacetradersDbContext dbContext)
    {
        var body = new RegisterBody
        {
            Email = "markdavison0@gmail.com",
            Faction = FactionSymbol.COSMIC,
            Symbol = "ZENO-1"
        };
        var existingAgent = await dbContext.Set<SpaceAgent>().AsNoTracking().FirstOrDefaultAsync(_ => _.Symbol == body.Symbol);
        if (existingAgent == null)
        {
            var createApi = new SpaceTradersApiClient(new AuthenticationContext(), new HttpClient());
            var registerResult = await createApi.RegisterAsync(body);
            await Task.Delay(1000);

            var newAgent = new SpaceAgent
            {
                Id = Guid.NewGuid(),
                Token = registerResult.Data.Token,
                Symbol = body.Symbol
            };

            createApi = new SpaceTradersApiClient(new AuthenticationContext { Token = newAgent.Token }, new HttpClient());
            var myAgent = await createApi.GetMyAgentAsync();
            newAgent.AccountId = myAgent.Data.AccountId;
            newAgent.Headquarters = myAgent.Data.Headquarters;

            await dbContext.AddAsync(newAgent);
            await dbContext.SaveChangesAsync();

            PrintObject("New Agent", newAgent);

            existingAgent = newAgent;
        }
        else
        {
            PrintObject("Existing Agent", existingAgent);
        }

        var api = new SpaceTradersApiClient(new AuthenticationContext { Token = existingAgent.Token }, new HttpClient());
        await SyncShips(dbContext, api);

        return existingAgent;
    }
}
