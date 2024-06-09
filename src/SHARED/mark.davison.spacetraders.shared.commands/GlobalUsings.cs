﻿global using mark.davison.common.server.abstractions.Authentication;
global using mark.davison.common.server.CQRS;
global using mark.davison.common.server.CQRS.Processors;
global using mark.davison.common.server.CQRS.Validators;
global using mark.davison.spacetraders.api.persistence;
global using mark.davison.spacetraders.core.Api;
global using mark.davison.spacetraders.core.Utility;
global using mark.davison.spacetraders.shared.models.dtos;
global using mark.davison.spacetraders.shared.models.dtos.Commands.AcceptContract;
global using mark.davison.spacetraders.shared.models.dtos.Commands.AddAccount;
global using mark.davison.spacetraders.shared.models.dtos.Commands.DeleteAccount;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchContracts;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchShip;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchShips;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchShipyard;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoint;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoints;
global using mark.davison.spacetraders.shared.models.dtos.Commands.OrbitShip;
global using mark.davison.spacetraders.shared.models.dtos.Commands.PurchaseShip;
global using mark.davison.spacetraders.shared.models.dtos.Shared;
global using mark.davison.spacetraders.shared.models.Entities;
global using mark.davison.spacetraders.shared.models.Helpers;
global using Microsoft.EntityFrameworkCore;
global using System.IdentityModel.Tokens.Jwt;
