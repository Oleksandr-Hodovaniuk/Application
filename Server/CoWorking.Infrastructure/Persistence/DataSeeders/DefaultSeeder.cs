using CoWorking.Application.Interfaces.Seeders;
using CoWorking.Core.Entities;
using CoWorking.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.DataSeeders;

internal class DefaultSeeder(CoWorkingDbContext dbContext) : ISeeder
{
    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        if (!await dbContext.Workspaces.AnyAsync(cancellationToken))
        {
            var icons = GetIcons();
            await dbContext.Icons.AddRangeAsync(icons, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);


            var workspaces_1 = GetWorkspaces_1();
            var workspaces_2 = GetWorkspaces_2();
            var workspaces_3 = GetWorkspaces_3();   

            var coworkings = new List<Coworking> 
            {
                new Coworking()
                {
                    CoworkingPicture = "coworking-1.png",
                    Name = "WorkClub Pechersk",
                    Description = "Modern coworking in the heart of Pechersk with quiet rooms and coffee on tap.",
                    Addresses = new Address { City = "Kyiv", Street = "Yaroslaviv Val St", BuildingNumber = 123 },
                    Workspaces = workspaces_1
                },
                new Coworking()
                {
                    CoworkingPicture = "coworking-2.png",
                    Name = "Creative Hub Lvivska",
                    Description = "A compact, design-focused space with open desks and strong community vibes.",
                    Addresses = new Address { City = "Kyiv", Street = "Lvivska Square", BuildingNumber = 12 },
                    Workspaces = workspaces_2
                },
                new Coworking()
                {
                    CoworkingPicture = "coworking-3.png",
                    Name = "TechNest Olimpiiska",
                    Description = "A high-tech space near Olimpiiska metro, perfect for team sprints and solo focus.",
                    Addresses = new Address { City = "Kyiv", Street = "Velyka Vasylkivska St,", BuildingNumber = 45 },
                    Workspaces= workspaces_3
                }
            };

            await dbContext.Coworkings.AddRangeAsync(coworkings, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var workspacesIcons = new List<WorkspaceIcon>
            {
                new WorkspaceIcon { WorkspaceId = workspaces_1[0].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[0].Id, IconId = icons[1].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[0].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[0].Id, IconId = icons[3].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_1[1].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[1].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[1].Id, IconId = icons[4].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_1[2].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[2].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[2].Id, IconId = icons[4].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_1[2].Id, IconId = icons[5].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_2[0].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[0].Id, IconId = icons[1].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[0].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[0].Id, IconId = icons[3].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_2[1].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[1].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[1].Id, IconId = icons[4].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_2[1].Id, IconId = icons[5].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_3[0].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[0].Id, IconId = icons[1].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[0].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[0].Id, IconId = icons[3].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_3[1].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[1].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[1].Id, IconId = icons[4].Id },

                new WorkspaceIcon { WorkspaceId = workspaces_3[2].Id, IconId = icons[0].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[2].Id, IconId = icons[2].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[2].Id, IconId = icons[4].Id },
                new WorkspaceIcon { WorkspaceId = workspaces_3[2].Id, IconId = icons[5].Id }
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
    private List<Workspace> GetWorkspaces_1()
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
            new Picture { Name = "open-space-1.png" },
            new Picture { Name = "open-space-2.png" },
            new Picture { Name = "open-space-3.png" },
            new Picture { Name = "open-space-4.png" }
        };
    }
    private List<Picture> GetPictures_2()
    {
        return new List<Picture>
        {
            new Picture { Name = "private-room-1.png" },
            new Picture { Name = "private-room-2.png" },
            new Picture { Name = "private-room-3.png" },
            new Picture { Name = "private-room-4.png" }
        };
    }
    private List<Picture> GetPictures_3()
    {
        return new List<Picture>
        {
            new Picture { Name = "meeting-room-1.png" },
            new Picture { Name = "meeting-room-2.png" },
            new Picture { Name = "meeting-room-3.png" },
            new Picture { Name = "meeting-room-4.png" }
        };
    }
    private List<Room> GetRooms_1()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 24, Bookings = GetBooking_1()}
        };
    }
    private List<Room> GetRooms_2()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 7, Bookings = GetBooking_2() },
            new Room { Capacity = 2, Quantity = 5, Bookings = GetBooking_3() },
            new Room { Capacity = 5, Quantity = 3, Bookings = GetBooking_4() },
            new Room { Capacity = 10, Quantity = 1, Bookings = GetBooking_5() }
        };
    }
    private List<Room> GetRooms_3()
    {
        return new List<Room>
        {
            new Room { Capacity = 10, Quantity = 4, Bookings = GetBooking_6() },
            new Room { Capacity = 20, Quantity = 1, Bookings = GetBooking_7() }
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
                StartDateTime = DateTime.Now.AddHours(24),
                EndDateTime = DateTime.Now.AddHours(48),
            }
        };
    }
    private List<Booking> GetBooking_2()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Sophia Collins",
                Email = "sophia.collins@example.com",
                StartDateTime = DateTime.Now.AddHours(5),
                EndDateTime = DateTime.Now.AddHours(10),
            }
        };
    }
    private List<Booking> GetBooking_3()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Lucas Wright",
                Email = "noah.mitchell@example.com",
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(3),
            }
        };
    }
    private List<Booking> GetBooking_4()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Noah Mitchell",
                Email = "noah.mitchell@example.com",
                StartDateTime = DateTime.Now.AddHours(3),
                EndDateTime = DateTime.Now.AddHours(5),
            }
        };
    }
    private List<Booking> GetBooking_5()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Isabella Reed",
                Email = "isabella.reed@example.com",
                StartDateTime = DateTime.Now.AddHours(16),
                EndDateTime = DateTime.Now.AddHours(72),
            }
        };
    }
    private List<Booking> GetBooking_6()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "Emily Carter",
                Email = "emily.reed@example.com",
                StartDateTime = DateTime.Now.AddHours(2),
                EndDateTime = DateTime.Now.AddHours(4),
            }
        };
    }
    private List<Booking> GetBooking_7()
    {
        return new List<Booking>
        {
            new Booking
            {
                Name = "James Bennett",
                Email = "james.reed@example.com",
                StartDateTime = DateTime.Now.AddHours(1),
                EndDateTime = DateTime.Now.AddHours(2),
            }
        };
    }

    private List<Workspace> GetWorkspaces_2()
    {
        return new List<Workspace>
        {
            new Workspace {
                Type = WorkspaceType.OpenSpace,
                Name = "Open space",
                Description = "An energetic open workspace ideal for freelancers" +
                " and small teams who thrive in a dynamic, community-driven" +
                " environment. Pick any free desk and enjoy the freedom to" +
                " work your way.",
                MaxBookingDuration = 30,
                Pictures = GetPictures_4(),
                Rooms = GetRooms_4()
            },
            new Workspace {
                Type = WorkspaceType.MeetingRoom,
                Name = "Meeting rooms",
                Description = "The perfect space for focused meetings, creative workshops," +
                " or impactful client presentations. Fully equipped with displays," +
                " whiteboards, and comfy chairs to help ideas flow and decisions happen.",
                MaxBookingDuration = 1,
                Pictures = GetPictures_5(),
                Rooms = GetRooms_5()
            }
        };
    }
    private List<Workspace> GetWorkspaces_3()
    {
        return new List<Workspace>
        {
            new Workspace {
                Type = WorkspaceType.OpenSpace,
                Name = "Open space",
                Description = "A dynamic open space made for creators and" +
                " small teams. Pick your spot, plug in, and get going — surrounded" +
                " by like-minded professionals.",
                MaxBookingDuration = 30,
                Pictures = GetPictures_6(),
                Rooms = GetRooms_6()
            },
            new Workspace {
                Type = WorkspaceType.PrivateRoom,
                Name = "Private rooms",
                Description = "Perfect for deep focus, video conferencing, or quick" +
                " team syncs. These enclosed rooms provide full privacy and come" +
                " in various sizes to suit your needs.",
                MaxBookingDuration = 30,
                Pictures = GetPictures_7(),
                Rooms = GetRooms_7()
            },
            new Workspace {
                Type = WorkspaceType.MeetingRoom,
                Name = "Meeting rooms",
                Description = "Created for efficient meetings, workshops, and client" +
                " presentations. These rooms are equipped with displays, whiteboards," +
                " and ergonomic seating to ensure smooth and productive sessions.",
                MaxBookingDuration = 1,
                Pictures = GetPictures_8(),
                Rooms = GetRooms_8()
            }
        };
    }
    private List<Picture> GetPictures_4()
    {
        return new List<Picture>
        {
            new Picture { Name = "open-space-5.png" },
            new Picture { Name = "open-space-6.png" },
            new Picture { Name = "open-space-7.png" },
            new Picture { Name = "open-space-8.png" }
        };
    }
    private List<Picture> GetPictures_5()
    {
        return new List<Picture>
        {
            new Picture { Name = "meeting-room-5.png" },
            new Picture { Name = "meeting-room-6.png" },
            new Picture { Name = "meeting-room-7.png" },
            new Picture { Name = "meeting-room-8.png" }
        };
    }
    private List<Picture> GetPictures_6()
    {
        return new List<Picture>
        {
            new Picture { Name = "open-space-9.png" },
            new Picture { Name = "open-space-10.png" },
            new Picture { Name = "open-space-11.png" },
            new Picture { Name = "open-space-12.png" }
        };
    }
    private List<Picture> GetPictures_7()
    {
        return new List<Picture>
        {
            new Picture { Name = "private-room-5.png" },
            new Picture { Name = "private-room-6.png" },
            new Picture { Name = "private-room-7.png" },
            new Picture { Name = "private-room-8.png" }
        };
    }
    private List<Picture> GetPictures_8()
    {
        return new List<Picture>
        {
            new Picture { Name = "meeting-room-9.png" },
            new Picture { Name = "private-room-10.png" },
            new Picture { Name = "private-room-11.png" },
            new Picture { Name = "private-room-12.png" }
        };
    }
    private List<Room> GetRooms_4()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 15 }
        };
    }
    private List<Room> GetRooms_5()
    {
        return new List<Room>
        {
            new Room { Capacity = 20, Quantity = 1 }
        };
    }
    private List<Room> GetRooms_6()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 40 }
        };
    }
    private List<Room> GetRooms_7()
    {
        return new List<Room>
        {
            new Room { Capacity = 1, Quantity = 10 },
            new Room { Capacity = 5, Quantity = 5 },
            new Room { Capacity = 8, Quantity = 3 }
        };
    }
    private List<Room> GetRooms_8()
    {
        return new List<Room>
        {
            new Room { Capacity = 20, Quantity = 1 },
            new Room { Capacity = 10, Quantity = 5 }
        };
    }
}


