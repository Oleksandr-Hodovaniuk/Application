using CoWorking.Application.Interfaces.Seeders;
using CoWorking.Core.Entities;
using CoWorking.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.DataSeeders;

internal class DefaultSeeder(CoWorkingDbContext dbContext) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if(!await dbContext.Workspaces.AnyAsync(cancellationToken))
        {
            var icons = GetIcons();
            await dbContext.Icons.AddRangeAsync(icons, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var workspaces = GetWorkspaces();
            await dbContext.Workspaces.AddRangeAsync(workspaces, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var workspacesIcons = new List<WorkspaceIcon>
            {
                new WorkspaceIcon { WorkspaceId = workspaces[0].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[0].Id, IconId = icons[1].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[0].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[0].Id, IconId = icons[3].Id },

                new WorkspaceIcon { WorkspaceId = workspaces[1].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[1].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[1].Id, IconId = icons[4].Id },

                new WorkspaceIcon { WorkspaceId = workspaces[2].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[2].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[2].Id, IconId = icons[4].Id },
                new WorkspaceIcon { WorkspaceId = workspaces[2].Id, IconId = icons[5].Id }
            };
            await dbContext.WorkspaceIcons.AddRangeAsync(workspacesIcons, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    private List<Icon> GetIcons()
    {
        return new List<Icon>
        {
            new Icon { Name = "air-conditioning.svg"},
            new Icon { Name = "gamepad.svg"},
            new Icon { Name = "wifi.svg"},
            new Icon { Name = "coffee.svg"},
            new Icon { Name = "headphones.svg"},
            new Icon { Name = "microphone.svg"},
            new Icon { Name = "user.svg"},
            new Icon { Name = "armchair.svg"},
            new Icon { Name = "clock.svg"},
            new Icon { Name = "calendar.svg"},
            new Icon { Name = "edit.svg"},
            new Icon { Name = "trash.svg"},
            new Icon { Name = "chevron.svg"},
            new Icon { Name = "x.svg"},
            new Icon { Name = "done.svg"},
            new Icon { Name = "big-done.svg"},
            new Icon { Name = "big-cancel.svg"},
            new Icon { Name = "big-trash.svg"},
        };
    }
    private List<Workspace> GetWorkspaces()
    {
        return new List<Workspace>
        {
            new Workspace {
                Type = WorkspaceType.OpenSpace,
                Name = "Open space",
                Description = "A vibrant shared area perfect for" +
                " freelancers or small teams who enjoy a collaborative" +
                " atmosphere. Choose any available desk and get to" +
                " work with flexibility and ease.",
                MaxBookingDuration = 30,
                Pictures = GetPictures_1(),
                Rooms = GetRooms_1()
            },
            new Workspace {
                Type = WorkspaceType.PrivateRoom,
                Name = "Private rooms",
                Description = "Ideal for focused work, video calls," +
                " or small team huddles. These fully enclosed rooms" +
                " offer privacy and come in a variety of sizes to fit" +
                " your needs.",
                MaxBookingDuration = 30,
                Pictures = GetPictures_2(),
                Rooms = GetRooms_2()
            },
            new Workspace {
                Type = WorkspaceType.MeetingRoom,
                Name = "Meeting rooms",
                Description = "Designed for productive meetings, workshops," +
                " or client presentations. Equipped with screens, whiteboards," +
                " and comfortable seating to keep your sessions running smoothly.",
                MaxBookingDuration = 1,
                Pictures = GetPictures_3(),             
                Rooms = GetRooms_3()
            }
        };
    }
    private List<Picture> GetPictures_1()
    {
        return new List<Picture>
        {
            new Picture { Name = "qew" },
            new Picture { Name = "asdwq" },
            new Picture { Name = "hytrh" },
            new Picture { Name = "zxcwe" },
            new Picture { Name = "wpkgl" }
        };
    }
    private List<Picture> GetPictures_2()
    {
        return new List<Picture>
        {
            new Picture { Name = "qwef" },
            new Picture { Name = "rtr" },
            new Picture { Name = "rtyrt" },
            new Picture { Name = "iuli" },
            new Picture { Name = "qwecf" }
        };
    }
    private List<Picture> GetPictures_3()
    {
        return new List<Picture>
        {
            new Picture { Name = "oui" },
            new Picture { Name = "tujhnv" },
            new Picture { Name = "qweygh" },
            new Picture { Name = "lmh" },
            new Picture { Name = "hotk" }
        };
    }
    private List<Room> GetRooms_1()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 24, Bookings = GetBooking_1() }
        };
    }
    private List<Room> GetRooms_2()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 7, Bookings = GetBooking_2() },
            new Room { Capacity = 2, Quantity = 5, Bookings = GetBooking_2() },
            new Room { Capacity = 5, Quantity = 3, Bookings = GetBooking_2() },
            new Room { Capacity = 10, Quantity = 1, Bookings = GetBooking_2() }
        };
    }
    private List<Room> GetRooms_3()
    {
        return new List<Room>
        {
            new Room { Capacity = 10, Quantity = 4, Bookings = GetBooking_3() },
            new Room { Capacity = 20, Quantity = 1, Bookings = GetBooking_2() }
        };
    }
    private List<Booking> GetBooking_1()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Ethan Morgan",
                Email = "ethan.morgan@example.com",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(5),
            },
            new Booking
            {
                Name = "Sophia Collins",
                Email = "sophia.collins@example.com",
                StartDateTime = DateTime.Now.AddHours(5),
                EndDateTime = DateTime.Now.AddHours(10),
            }
        };
    }
    private List<Booking> GetBooking_2()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Lucas Wright",
                Email = "noah.mitchell@example.com",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(3),
            }
        };
    }
    private List<Booking> GetBooking_3()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Noah Mitchell",
                Email = "noah.mitchell@example.com",
                StartDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddHours(5),
            },
            new Booking
            {
                Name = "Isabella Reed",
                Email = "isabella.reed@example.com",
                StartDateTime = DateTime.Now.AddHours(8),
                EndDateTime = DateTime.Now.AddHours(16),
            }
        };
    }
}
