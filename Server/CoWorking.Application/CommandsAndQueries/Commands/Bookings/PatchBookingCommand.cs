using CoWorking.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoWorking.Application.CommandsAndQueries.Commands.Bookings;

public record PatchBookingCommand(int id, BookingPatchDTO dto) : IRequest;
