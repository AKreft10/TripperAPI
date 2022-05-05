using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Entities;

namespace TripperAPI
{
    public class DbSeeder
    {
        private readonly DatabaseContext _context;

        public DbSeeder(DatabaseContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if(_context.Database.CanConnect())
            {
                if(!_context.Places.Any())
                {
                    var places = GetPlaces();
                    _context.Places.AddRange(places);
                    _context.SaveChanges();
                }
            }
        }

        private List<Place> GetPlaces()
        {
            List<Place> places = new List<Place>()
            {
                new Place
                {
                    Name = "Louvre Museum",
                    Description = "The Louvre, or the Louvre Museum, is the world's most-visited museum, and a historic landmark in Paris, France. It is the home of some of the best-known works of art, including the Mona Lisa and the Venus de Milo.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "France",
                        City = "Paris",
                        Street = "Rue de Rivoli",
                        PostalCode = "75001",
                        Latitude = 48.86065695904424,
                        Longitude = 2.3376225404954907
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Author = "Saad Achwa",
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "One of the best places and 100% must visit  place during the season, though you have to be careful of social distancing because Covid19 is high peak once again, may Hid protect all humanity and out loved ones.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://cdn-imgix.headout.com/mircobrands-content/image/4e9ebbf7ff686d29a41f4af39e8386ea-louvre%20museum.jpg?auto=compress%2Cformat&h=573&q=75&fit=crop&ar=16%3A9&fm=pjpg",
                                    GalleryMember=true, 
                                    Author = "Arthur Mason"
                                }
                            }
                            
                        },

                        new Review()
                        {
                            Author = "Katherine Mojica",
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "One of my favorite places in France. It's breathtaking, the history and every room has so much to tell. You'll need one whole day to really see most of it, because the museum is really big. You must plan to be there for minimum 4 hours.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://cdn.britannica.com/02/121002-050-92DB902F/Louvre-Museum-pyramid-Paris-Pei-IM.jpg",
                                    GalleryMember = true,
                                    Author = "Caleb Evans"
                                }
                            }
                        }
                    }
                }
            };

            return places;
        }
    }
}
