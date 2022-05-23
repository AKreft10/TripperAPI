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

                if(!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
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
                },
                new Place
                {
                    Name = "Eiffel Tower",
                    Description = "The Eiffel Tower is a wrought-iron lattice tower on the Champ de Mars in Paris, France. It is named after the engineer Gustave Eiffel, whose company designed and built the tower.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "France",
                        City = "Paris",
                        Street = "Champ de Mars, 5 Av. Anatole France",
                        PostalCode = "75007",
                        Latitude = 48.85838772614254,
                        Longitude = 2.294481298176074
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "I mean, come on...it's the Eiffel Tower! This is a magical place. The view of Paris from the top is absolutely breathtaking.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://upload.wikimedia.org/wikipedia/commons/0/09/Eiffel_Tower_from_Champ-de-Mars%2C_Paris_5_February_2019.jpg" ,
                                    GalleryMember=true,
                                    Author = "Chris Petrik"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "The most spectacular and wonderful place in Paris. The Eiffel tower is historical place, but looks modern at the same time.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                   Url="https://www.toureiffel.paris/sites/default/files/styles/crop_principale/public/2022-01/Illumination%20COP21%20One%20heart%20One%20tree%20%C2%A9%20E.Livinec-_2.jpg?itok=Q4cMBJQ7",
                                   GalleryMember = true,
                                   Author = "Rudro Rana"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Notre Dame Cathedral",
                    Description = "Notre-Dame Cathedral consists of a choir and apse, a short transept, and a nave flanked by double aisles and square chapels.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "France",
                        City = "Paris",
                        Street = "6 Parvis Notre-Dame - Pl. Jean-Paul II",
                        PostalCode = "75004",
                        Latitude = 48.852996417300076,
                        Longitude = 2.349869911715236
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Even though one can't tour the cathedral, it was still breath-taking to walk around it. Plus, we visited the excavation which I had never seen before. Well worth it.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://static.dw.com/image/48336209_303.jpg",
                                    GalleryMember=true,
                                    Author = "Mark Glom"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "My favorite cathedral in all of Europe. Highly recommend a private tour to learn all of the meaning of the art and history of construction. Can't wait for it to be rebuilt.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://static.dw.com/image/48336209_303.jpg",
                                    GalleryMember = true,
                                    Author = "ScooterM52"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Jardin des Tuileries",
                    Description = "Centrally located between the Louvre and the Place de la Concorde, the Jardin des Tuileries is a free public garden that spans approximately 55 acres. Though it was initially designed solely for the use of the royal family and court, the park was added to the UNESCO World Heritage list in 1991 (as part of the Banks of the Seine) and has been open to the public since the 17th century.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "France",
                        City = "Paris",
                        Street = "Place de la Concorde",
                        PostalCode = "75001",
                        Latitude = 48.863647355917614,
                        Longitude = 2.327462780509457
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Beautiful gardens right in the heart of Paris. Take a stroll, take a nap, enjoy a bite to eat or a cup of coffee in these beautiful gardens. Walking distance from many Paris attractions. Definitely recommend.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://dynamic-media-cdn.tripadvisor.com/media/photo-o/06/08/d8/a8/jardin-des-tuileries.jpg?w=1200&h=1200&s=1",
                                    GalleryMember=true,
                                    Author = "Grimx32"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 3,
                            Content = "This grand square seems somehow underwhelming now that the Luxor obelisk is under renovations.Still, one cannot ignore the history sheer size of the square even now.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://upload.wikimedia.org/wikipedia/commons/b/b6/Paris_Place_de_la_Concorde_2010-04-06_16.20.59.jpg",
                                    GalleryMember = true,
                                    Author = "TheShis"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Le Marais",
                    Description = "Straddling the 3ème and 4ème arrondissements (districts), Le Marais is one of Paris' oldest and coolest districts – so cool, in fact, that French writer Victor Hugo (author of The Hunchback of Notre Dame and Les Misérables) called it home.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "France",
                        City = "Paris",
                        Street = "Le Marais 3e Arrondissement",
                        PostalCode = "75003",
                        Latitude = 48.86334462555913,
                        Longitude = 2.3594564177216824
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Tiny streets, small shops, plentiful restaurants, and endless charm. This is a wonderful district to meander by foot, and maybe by bicycle.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://www.theshopkeepers.com/wp-content/uploads/2021/01/marais.jpg",
                                    GalleryMember=true,
                                    Author = "motohigh"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Only problem is where to start and how to finish. Great shops, boulangeries, cheese mongers and chocolatiers. Cannot wait to return!",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://en.parisinfo.com/var/otcp/sites/images/node_43/node_51/node_232/le-marais-rue-vieille-du-temple-%7C-630x405-%7C-%C2%A9-studio-ttg/19408876-1-fre-FR/Le-Marais-rue-vieille-du-temple-%7C-630x405-%7C-%C2%A9-Studio-TTG.jpg",
                                    GalleryMember = true,
                                    Author = "Sue H"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Museo Nacional del Prado",
                    Description = "A truly world-class museum, the Museo Nacional del Prado has a collection of more than 8,000 paintings and 700 sculptures. Among its extensive assortment of artworks are many masterpieces, including celebrated paintings that rival the most famous works of the Louvre Museum in Paris.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Spain",
                        City = "Madrid",
                        Street = "C. de Ruiz de Alarcón, 23",
                        PostalCode = "28014",
                        Latitude = 40.41378994811165,
                        Longitude = -3.692127102055045
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "The place is huge. We had several works of art we wanted to view. Most of the day was spent on the scavenger hunt for the artworks shown in the brochure. Well worth the visit.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/54/Museo_del_Prado_frente.JPG/640px-Museo_del_Prado_frente.JPG",
                                    GalleryMember=true,
                                    Author = "Senioronabudget"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "This is a very big art gallery with lots to see and full history it gets lots of visitors so it’s a busy place, it also has a gift shop that is expensive but that’s expected.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://www.inexhibit.com/wp-content/uploads/2014/07/Museo-Nacional-del-Prado-Madrid-3.jpg",
                                    GalleryMember = true,
                                    Author = "scott s"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Buen Retiro Park and the Crystal Palace",
                    Description = "The Buen Retiro Park (Parque del Retiro) is an oasis of peace in the heart of Madrid. This lush and beautifully manicured park offers an escape from the hustle and bustle of the city. The park encompasses more than 125 hectares and is shaded by over 15,000 trees.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Spain",
                        City = "Madrid",
                        Street = "P. de Cuba, 4",
                        PostalCode = "28009",
                        Latitude = 40.4136429168945,
                        Longitude = -3.682039702055058
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "One of the must sees in the Park. Modelled to an extent on the London Crystal Palace of the great exhibition this is an impressive construction of iron and glass which sometimes contains a small exhibition. Best viewed on a sunny day from across the pond when the building and the trees are reflected in the water.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://sumfinity.com/wp-content/uploads/2018/03/Crystal-Palace-Buen-Retiro-Park-Madrid-Spain.jpg",
                                    GalleryMember=true,
                                    Author = "Allan C"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "We visited the Park only to see this former glass greenhouse. The raw beauty of it, the iron and glass set amongst the trees was stunning. There was a short line to get in, but it went quickly and there is no fee. Lots of unique photo ops inside, the architecture is unique onto itself.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://assets.puzzlefactory.pl/puzzle/304/612/original.jpg",
                                    GalleryMember = true,
                                    Author = "Lee Ann Murray"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Basílica de la Sagrada Família",
                    Description = "The Basílica de la Sagrada Família stands in the northern part of the city, dominating its surroundings with its 18 spindly towers soaring high above all the other buildings. One of Europe's most unconventional churches, this amazing monument is designated as a UNESCO World Heritage Site.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Spain",
                        City = "Barcelona",
                        Street = "C/ de Mallorca, 401",
                        PostalCode = "08013",
                        Latitude = 41.403670116095235,
                        Longitude = 2.174366526812656
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "If you come to Barcelona, go here! It might feel like a hefty price tag for a cathedral entrance in Europe, but it is 100% worth it!",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://images.theconversation.com/files/404163/original/file-20210603-13-1uni8db.jpg?ixlib=rb-1.1.0&q=45&auto=format&w=1200&h=1200.0&fit=crop",
                                    GalleryMember=true,
                                    Author = "Greg"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Must visit. Our guide said we had really good sun that day so it really depends on the good weather to enjoy the kaleidoscope colours illuminated into the church!",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://d2prydcqrq5962.cloudfront.net/image/journal/article?img_id=801614&t=1624602073691",
                                    GalleryMember = true,
                                    Author = "Jergen3"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Barri Gòtic",
                    Description = "For 2,000 years, the Gothic Quarter has been the spiritual and secular center of the city. Relics of ancient Roman buildings are still found here, but the Middle Ages are best represented by the historic monuments packed into this quarter.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Spain",
                        City = "Barcelona",
                        Street = "-",
                        PostalCode = "08002",
                        Latitude = 41.38280897433782,
                        Longitude = 2.174946062385823
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Enjoyed wandering around the Gothic Quarter and just taking n the beauty of the buildings. Well worth a visit.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ5dqRnUtc9B6c1b2SADSPIEtZp53zvIOzzQQ&usqp=CAU",
                                    GalleryMember=true,
                                    Author = "RosyHER3"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "when the children are happy then we are happy, good atmosphere in this Gothic district, many visits to exhibitions",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://www.barcelona.de/images/barri-gotic/480-barri-gotic-stadtmauer.jpg",
                                    GalleryMember = true,
                                    Author = "Thsalco"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Catedral de Toledo",
                    Description = "With its soaring tower and marvelous Gothic architecture, Toledos cathedral is one of the most important Christian landmarks in Spain. The cathedral was built in the 13th century on the site of a Muslim mosque next to La Judería (Jewish quarter).",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Spain",
                        City = "Toledo",
                        Street = "Calle Cardenal Cisneros, 1",
                        PostalCode = "45002",
                        Latitude = 39.85710861537319,
                        Longitude = -4.023558073235904
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Incredible building on every scale- such an undertaking for those who began it, knowing they would never see anything near its completion",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQkqIGngopR2aOMqIvVXDoT9WVsZHIl0jOY_g&usqp=CAU",
                                    GalleryMember=true,
                                    Author = "sheffieldeats"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "One of the most imposing cathedrals in Spain, and a superb example of Gothic architecture.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-pOMl60luKKxkPAEKyAiNqkS1UOa4nTBcTg&usqp=CAU",
                                    GalleryMember = true,
                                    Author = "majeo"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "The Brandenburg Gate",
                    Description = "Berlin's most famous historic landmark is undoubtedly the Brandenburg Gate (Brandenburger Tor). Once a symbol of a divided nation, it now stands as a symbol of unity and peace.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Germany",
                        City = "Berlin",
                        Street = "Pariser Platz",
                        PostalCode = "10117",
                        Latitude = 52.51644759488455,
                        Longitude = 13.377693369475782
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Iconic gate to photograph. Come for pictures and enjoy the specular art. It's not too far and a nice walk from other nearby attractions. There were also people blowing bubbles in the air to help you make your photos look beautiful.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a6/Brandenburger_Tor_abends.jpg/1200px-Brandenburger_Tor_abends.jpg",
                                    GalleryMember=true,
                                    Author = "Nathan Jenkins"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "I had an amazing time here, beautiful views and serene vicinity. If you are a photographer then you are going to have a time of your life.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://cdn.britannica.com/39/6839-050-27891400/Brandenburg-Gate-Berlin.jpg",
                                    GalleryMember = true,
                                    Author = "Riley Hopkins"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "The Rebuilt Reichstag",
                    Description = "The Reichstag (Reichstagsgebäude) was originally completed in 1894 where the Neo-Renaissance palace served as the home of the German Empire's Imperial Diet until it burned in 1933.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Germany",
                        City = "Berlin",
                        Street = "Platz der Republik 1",
                        PostalCode = "11011",
                        Latitude = 52.51872790050797,
                        Longitude = 13.376197927116714
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 4,
                            Content = "The excursion itself is audioguided tour in the dome while you'll watch the neighboring attractions and hear about them and about some historic facts. It's free of charge but advance registration in a small building near the entrance is required.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/14/Berlin_reichstag_CP.jpg/1200px-Berlin_reichstag_CP.jpg",
                                    GalleryMember=true,
                                    Author = "Tabalo"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "I knew we had to book to visit the Dome, but was not aware that we were meant to have booked 3 days in advance. Even still, we took our chances on our first day in Berlin, hoping we might be lucky, as this was something I really wanted to see.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://www.berlin.de/binaries/asset/image_assets/6486153/source/1646302570/624x468/",
                                    GalleryMember = true,
                                    Author = "Kate S"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "Museum Island",
                    Description = "Sandwiched between the River Spree and the Kupfergraben in a 400-meter-long canal, Spree Island — better known as Museum Island (Museumsinsel) — is one of the city's most important UNESCO World Heritage Sites.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Germany",
                        City = "Berlin",
                        Street = "-",
                        PostalCode = "-",
                        Latitude = 52.51706638284033,
                        Longitude = 13.401926397084221
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "If you have little time in Berlin then, after Brandenburg gate you must head to Museum Island. It houses five museums and also the famous Berlin Cathedral.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://c8.alamy.com/comp/PRTC8J/bode-museum-museum-island-berlin-germany-PRTC8J.jpg",
                                    GalleryMember=true,
                                    Author = "Toby Galloway"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 3,
                            Content = "Amazing place to chill and meet history art and much more. It was cold so i guess in summer it would be better. A must go to in Berlin",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://ak.jogurucdn.com/media/image/p25/place-2014-10-09-15-0b89f72169fe467441ff97b525a71e29.jpg",
                                    GalleryMember = true,
                                    Author = "Rahaf Walid"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "The Berlin Wall Memorial",
                    Description = "The Berlin Wall originated in 1961 when East Germany sealed off that half of the city to prevent citizens from fleeing to West Germany. By the time it was torn down in 1989, the four-meter-high wall extended 155 kilometers, dissected 55 streets, and possessed 293 observation towers and 57 bunkers.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Germany",
                        City = "Berlin",
                        Street = "Bernauer Str. 111",
                        PostalCode = "13355",
                        Latitude = 52.535094619209374,
                        Longitude = 13.390184080948963
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "An interesting place to walk around. Remnants of the wall, there are areas marked out where old pavements were, the lights used to illuminate the wall to.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://www.stiftung-berliner-mauer.de/sites/default/files/styles/xl/public/media/standorte/gbm/allgemein/1_SBM_Gedenksta%CC%88tteBerlinerMauer_Bild_04.jpg?itok=6Humivp8",
                                    GalleryMember=true,
                                    Author = "Aiden Patel"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Impressive to understand how this wall has been set up and how the people lived around it. Besides the memorial the whole street is filled with memories and it's great to have it to remind our selves of the history and never forget",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://static.dw.com/image/18040671_101.jpg",
                                    GalleryMember = true,
                                    Author = "Billy Holmes"
                                }
                            }
                        }
                    }
                },

                new Place
                {
                    Name = "German Historical Museum",
                    Description = "The German Historical Museum, known by the acronym DHM, is a museum in Berlin, Germany devoted to German history.",
                    Address = new Address
                    {
                        Continent = "Europe",
                        Country = "Germany",
                        City = "Berlin",
                        Street = "Unter den Linden 2",
                        PostalCode = "10117",
                        Latitude = 52.51816820132206,
                        Longitude = 13.396638762046642
                    },
                    Reviews = new List<Review>()
                    {
                        new Review
                        {
                            Created = DateTime.Now,
                            Rating = 5,
                            Content = "Great exhibition on commissioned art in Third Reich and after exploring continuance of purpose. Awesome building.",
                            Photos = new List<Photo>()
                            {
                                new Photo
                                {
                                    Url = "https://www.dhm.de/assets/_processed_/8/c/csm_uber_uns_01_8f0806ff17.png",
                                    GalleryMember=true,
                                    Author = "leonard stone"
                                }
                            }

                        },

                        new Review()
                        {
                            Created = DateTime.Now,
                            Rating = 4,
                            Content = "Nice expensive of arts and politics. Also good to have student price of 4€, free wifi and toilets.",
                            Photos = new List<Photo>()
                            {
                                new Photo()
                                {
                                    Url = "https://cndarcdn.scdn3.secure.raxcdn.com/m/0/dhm-1920x800.jpg",
                                    GalleryMember = true,
                                    Author = "George Lane"
                                }
                            }
                        }
                    }
                },


            };
            return places;

        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = "User"
                    },

                    new Role()
                    {
                        Name = "Place owner"
                    },

                    new Role()
                    {
                        Name = "Administrator"
                    }
                };
            return roles;
        }
    }
}
