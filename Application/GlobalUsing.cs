global using Application.Common;
global using Application.Common.Extensions;
global using Application.Contracts;
global using Application.Dto.Order;
global using Application.Model;
global using Application.Model.Tara.Request;
global using Application.Model.Tara.Response;
global using Application.Services.Pos;
global using Application.Services.Tara;
global using Application.ViewModel.PosViewModels;
global using Domain.Entities;
global using Mapster;
global using Application.ViewModel.PurchaseViewModels;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Serilog;
global using Serilog.Sinks.MSSqlServer;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Globalization;
global using System.Linq.Expressions;
global using System.Reflection;

