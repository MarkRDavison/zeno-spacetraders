﻿global using mark.davison.common.server.abstractions.Authentication;
global using mark.davison.common.server.CQRS;
global using mark.davison.common.server.CQRS.Processors;
global using mark.davison.common.server.CQRS.Validators;
global using mark.davison.spacetraders.api.persistence;
global using mark.davison.spacetraders.core.Api;
global using mark.davison.spacetraders.shared.models.dtos;
global using mark.davison.spacetraders.shared.models.dtos.Commands.AddAccount;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchContracts;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchShips;
global using mark.davison.spacetraders.shared.models.dtos.Commands.FetchWaypoints;
global using mark.davison.spacetraders.shared.models.dtos.Shared;
global using mark.davison.spacetraders.shared.models.Entities;
global using Microsoft.EntityFrameworkCore;
global using System.IdentityModel.Tokens.Jwt;
