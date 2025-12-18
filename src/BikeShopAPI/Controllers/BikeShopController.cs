using Microsoft.AspNetCore.Mvc;
using BikeShopAPI.Entities;
using System.Diagnostics;

namespace BikeShopAPI.Controllers
{
    [Route("api/bikeshop")]
    [ApiController]

    public class BikeShopController : ControllerBase
    {
        private readonly ILogger<BikeShopController> _logger;

        public BikeShopController(ILogger<BikeShopController> logger)
        {
            _logger = logger;
        }
        private readonly List<BikeShop> BikeShops = new()
        {
            new BikeShop
            {
            Id = 1,
            Name = "Fast Wheels",
            Description = "Your one-stop shop for high-speed bikes.",
            Category = "Road",
            HasDelivery = true,
            AddressId = 101,
            Status = ShopStatus.Closed,
            ImageUrl = "https://example.com/images/fast-wheels.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 1,
                Brand = "Cannondale",
                Model = "Synapse",
                Description = "A road bike that's light, stiff, fast and surprisingly comfortable.",
                Price = 2000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 400,
                ShopId = 1,
                RideLogSignatures = new List<string> { "17091908-DEP-ARR-WB001", "18092015-DEP-ARR-WB001" }
                },
                new() {
                Id = 2,
                Brand = "Specialized",
                Model = "Roubaix",
                Description = "A performance road bike that's both comfortable and fast.",
                Price = 2500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 750,
                ShopId = 1,
                RideLogSignatures = new List<string> { "19091210-DEP-ARR-WB002", "20092112-DEP-ARR-WB002" }
                }
            }
            },
            new BikeShop
            {
            Id = 2,
            Name = "Eco Riders",
            Description = "Environmentally friendly bikes for the eco-conscious rider.",
            Category = "Electric",
            HasDelivery = true,
            AddressId = 102,
            Status = ShopStatus.Open,
            ImageUrl = "https://example.com/images/eco-riders.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 3,
                Brand = "Gazelle",
                Model = "CityZen",
                Description = "A comfortable and fast electric bike for city riding.",
                Price = 3000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1000,
                ShopId = 2,
                RideLogSignatures = new List<string> { "21100814-DEP-ARR-WB003", "22101020-DEP-ARR-WB003" }
                },
                new() {
                Id = 4,
                Brand = "Riese & Müller",
                Model = "Supercharger2",
                Description = "A high-performance electric bike for long-distance riding.",
                Price = 4000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1300,
                ShopId = 2,
                RideLogSignatures = new List<string> { "23110909-DEP-ARR-WB004", "24111115-DEP-ARR-WB004" }
                }
            }
            },
            new BikeShop
            {
            Id = 3,
            Name = "City Cruisers",
            Description = "Perfect bikes for the urban jungle.",
            Category = "Urban",
            HasDelivery = false,
            AddressId = 103,
            Status = ShopStatus.Renovating,
            ImageUrl = "https://example.com/images/city-cruisers.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 5,
                Brand = "VanMoof",
                Model = "S3",
                Description = "A stylish and smart bike for city riding.",
                Price = 1500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1500,
                ShopId = 3,
                RideLogSignatures = new List<string> { "25120711-DEP-ARR-WB005", "26121318-DEP-ARR-WB005" }
                },
                new() {
                Id = 6,
                Brand = "Tern",
                Model = "GSD",
                Description = "A compact and powerful e-bike for city riding.",
                Price = 2000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1200,
                ShopId = 3,
                RideLogSignatures = new List<string> { "27010516-DEP-ARR-WB006", "28011422-DEP-ARR-WB006" }
                }
            }
            },
            new BikeShop
            {
            Id = 4,
            Name = "Mountain Trails",
            Description = "Specialized in mountain bikes for trail enthusiasts.",
            Category = "Mountain",
            HasDelivery = true,
            AddressId = 104,
            Status = ShopStatus.Open,
            ImageUrl = "https://example.com/images/mountain-trails.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 7,
                Brand = "Trek",
                Model = "Fuel EX",
                Description = "A versatile mountain bike for trail riding.",
                Price = 3500,
                ForkTravel = 130,
                RearTravel = 120,
                WaterInBidon = 800,
                ShopId = 4,
                RideLogSignatures = new List<string> { "29020813-DEP-ARR-WB007", "30021619-DEP-ARR-WB007" }
                },
                new() {
                Id = 8,
                Brand = "Santa Cruz",
                Model = "Hightower",
                Description = "A high-performance trail bike for aggressive riding.",
                Price = 4500,
                ForkTravel = 150,
                RearTravel = 145,
                WaterInBidon = 900,
                ShopId = 4,
                RideLogSignatures = new List<string> { "31031014-DEP-ARR-WB008", "01041721-DEP-ARR-WB008" }
                }
            }
            },
            new BikeShop
            {
            Id = 5,
            Name = "Wheel Brothers Downtown",
            Description = "Premium bike shop in the heart of the city.",
            Category = "Premium",
            HasDelivery = true,
            AddressId = 105,
            Status = ShopStatus.Open,
            ImageUrl = "https://example.com/images/wheel-brothers-downtown.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 9,
                Brand = "Pinarello",
                Model = "Dogma F",
                Description = "A professional racing bike used by world champions.",
                Price = 8000,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 750,
                ShopId = 5,
                RideLogSignatures = new List<string> { "02050912-DEP-ARR-WB009", "03051518-DEP-ARR-WB009" }
                },
                new() {
                Id = 10,
                Brand = "Cervélo",
                Model = "R5",
                Description = "A lightweight and aerodynamic road bike.",
                Price = 7500,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 800,
                ShopId = 5,
                RideLogSignatures = new List<string> { "04061107-DEP-ARR-WB010", "05061820-DEP-ARR-WB010" }
                }
            }
            },
            new BikeShop
            {
            Id = 6,
            Name = "Wheel Brothers Adventure",
            Description = "Your destination for gravel and adventure bikes.",
            Category = "Gravel",
            HasDelivery = true,
            AddressId = 106,
            Status = ShopStatus.Open,
            ImageUrl = "https://example.com/images/wheel-brothers-adventure.jpg",
            Bikes = new List<Bike>
            {
                new() {
                Id = 11,
                Brand = "Salsa",
                Model = "Warbird",
                Description = "A versatile gravel bike for all-terrain adventures.",
                Price = 3202,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 1000,
                ShopId = 6,
                RideLogSignatures = new List<string> { "06071409-DEP-ARR-WB011", "07072015-DEP-ARR-WB011" }
                },
                new() {
                Id = 12,
                Brand = "Canyon",
                Model = "Grizl",
                Description = "A capable gravel bike designed for long-distance riding.",
                Price = 2800,
                ForkTravel = 0,
                RearTravel = 0,
                WaterInBidon = 950,
                ShopId = 6,
                RideLogSignatures = new List<string> { "08081210-DEP-ARR-WB012", "09081617-DEP-ARR-WB012" }
                }
            }
            }

        };

        [HttpGet]
        public ActionResult<List<BikeShop>> GetAll()
        {
            _logger.LogInformation("GET all 🚲🚲🚲 NO PARAMS 🚲🚲🚲");

            return Ok(BikeShops);
        }

        /// <summary>
        /// Obtiene una tienda de bicicletas específica por su identificador único.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite recuperar la información completa de una tienda de bicicletas,
        /// incluyendo su catálogo de bicicletas disponibles, utilizando su ID como parámetro de búsqueda.
        /// 
        /// Ejemplo de solicitud:
        /// 
        ///     GET /api/bikeshop/1
        ///     
        /// </remarks>
        /// <param name="id">El identificador único de la tienda de bicicletas que se desea obtener.</param>
        /// <returns>
        /// Un objeto <see cref="ActionResult{BikeShop}"/> que contiene:
        /// <list type="bullet">
        /// <item><description>200 OK: La tienda de bicicletas con toda su información si existe.</description></item>
        /// <item><description>404 NotFound: Si no se encuentra una tienda con el ID especificado.</description></item>
        /// </list>
        /// </returns>
        /// <response code="200">Retorna la tienda de bicicletas solicitada con todos sus detalles, incluyendo la lista de bicicletas disponibles.</response>
        /// <response code="404">No se encontró ninguna tienda de bicicletas con el ID especificado.</response>
        [HttpGet("{id}")]
        public ActionResult<BikeShop> GetById(int id)
        {
            var bikeshop = BikeShops.Find(p => p.Id == id);

            if (bikeshop == null)
            {
                return NotFound();
            }

            return Ok(bikeshop);
        }
        
        [HttpPost]
        public ActionResult<BikeShop> Create(BikeShop bikeShop)
        {
            BikeShops.Add(bikeShop);
            return CreatedAtAction(nameof(GetById), new { id = bikeShop.Id }, bikeShop);
        }

        /// <summary>
        /// Actualiza completamente una tienda de bicicletas existente.
        /// </summary>
        /// <remarks>
        /// Este endpoint permite actualizar todos los campos de una tienda de bicicletas específica.
        /// Se reemplazarán todos los valores con los proporcionados en el objeto de actualización.
        /// 
        /// Ejemplo de solicitud:
        /// 
        ///     PUT /api/bikeshop/1
        ///     {
        ///         "id": 1,
        ///         "name": "Fast Wheels Updated",
        ///         "description": "Nueva descripción actualizada",
        ///         "category": "Road",
        ///         "hasDelivery": true,
        ///         "addressId": 101,
        ///         "status": "Open"
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">El identificador único de la tienda de bicicletas que se desea actualizar.</param>
        /// <param name="updatedBikeShop">Objeto que contiene todos los datos actualizados de la tienda de bicicletas.</param>
        /// <returns>
        /// Un objeto <see cref="ActionResult"/> que contiene:
        /// <list type="bullet">
        /// <item><description>204 NoContent: Si la actualización fue exitosa.</description></item>
        /// <item><description>404 NotFound: Si no se encuentra una tienda con el ID especificado.</description></item>
        /// </list>
        /// </returns>
        /// <response code="204">La tienda de bicicletas fue actualizada exitosamente.</response>
        /// <response code="404">No se encontró ninguna tienda de bicicletas con el ID especificado.</response>
        [HttpPut("{id}")]
        public ActionResult Update(int id, BikeShop updatedBikeShop)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id); 
            if (bikeShop != null)
            {
                bikeShop.Name = updatedBikeShop.Name;
                bikeShop.Description = updatedBikeShop.Description;
                bikeShop.Category = updatedBikeShop.Category;
                bikeShop.HasDelivery = updatedBikeShop.HasDelivery;
                bikeShop.AddressId = updatedBikeShop.AddressId;
                bikeShop.Status = updatedBikeShop.Status;

                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        // update bike shop details         
        [HttpPut("{id}/details")]
        public ActionResult UpdateDetails(int id, string description, string category, bool hasDelivery)
        {               
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop != null)
            {
                bikeShop.Description = description;
                bikeShop.Category = category;
                bikeShop.HasDelivery = hasDelivery;

                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }        

        // delete bike shop
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id);
            if (bikeShop != null)
            {
                BikeShops.Remove(bikeShop);
                return NoContent();
            }
            else
            {           
                return NotFound();
            }           

        }



        [HttpPost("{id}/status")]
        public ActionResult UpdateShopStatus(int id, ShopStatus newStatus)
        {
            var bikeShop = BikeShops.Find(b => b.Id == id);
            
            if (bikeShop == null)
            {
                return NotFound("Bike shop not found.");
            }

            var validationError = ValidateStatusTransition(bikeShop.Status, newStatus);
            if (validationError != null)
            {
                return BadRequest(validationError);
            }

            bikeShop.Status = newStatus;
            _logger.LogInformation("Bike shop {ShopId} status updated from {OldStatus} to {NewStatus}", 
                id, bikeShop.Status, newStatus);

            return Ok($"Bike shop status updated to {newStatus}.");
        }

        private string? ValidateStatusTransition(ShopStatus currentStatus, ShopStatus newStatus)
        {
            var invalidTransitions = new Dictionary<(ShopStatus From, ShopStatus To), string>
            {
                { (ShopStatus.Renovating, ShopStatus.Open), "Cannot reopen, shop is currently renovating." },
                { (ShopStatus.Renovating, ShopStatus.Closed), "Cannot close, shop is currently renovating." },
                { (ShopStatus.Open, ShopStatus.Renovating), "Shop must be closed before starting renovations." }
            };

            return invalidTransitions.TryGetValue((currentStatus, newStatus), out var errorMessage) 
                ? errorMessage 
                : null;
        }

        [HttpPost("{shopId}/bikes/{bikeId}/takeRide/{rideLength}")]
        public ActionResult TakeRide(int shopId, int bikeId, int rideLength)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            if (bikeShop == null)
            {
                return NotFound("Bike shop not found.");
            }

            var bike = bikeShop.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            int waterConsumptionPerKm = 50;
            int totalWaterNeeded = rideLength * waterConsumptionPerKm;

            if (bike.WaterInBidon < totalWaterNeeded)
            {
                int maxDistance = bike.WaterInBidon / waterConsumptionPerKm;
                return BadRequest($"Insufficient water. Bike can only ride {maxDistance} kilometers with current water level.");
            }

            bike.WaterInBidon -= totalWaterNeeded;

            return Ok($"Bike rode {rideLength} kilometers. Water left in bidon: {bike.WaterInBidon} ml.");
        }

        [HttpPost("{shopId}/bikes/{bikeId}/bikeMalfunction")]
        public ActionResult BikeMalfunction(int shopId, int bikeId)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);

            if (bikeShop == null)
            {
                return NotFound("Bike shop not found.");
            }

            var bike = bikeShop.FindBikeById(bikeId);

            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            // Simulate a malfunction recovery without recursion
            _logger.LogWarning("Bike malfunction detected for bike {BikeId} in shop {ShopId}", bikeId, shopId);
            
            // Here you would implement actual malfunction handling logic
            // For example: reset bike state, notify maintenance, etc.

            return Ok($"Recovered from bicycle malfunction for bike {bike.Brand} {bike.Model}.");
        }

        [HttpGet("{shopId}/bikes/{bikeId}/details")]
        public ActionResult GetBikeDetails(int shopId, int bikeId)
        {
            var bikeShop = BikeShops.FirstOrDefault(b => b.Id == shopId);

            if (bikeShop == null)
            {
                return NotFound(new { Message = "Bike shop not found." });
            }

            var bike = bikeShop.Bikes?.FirstOrDefault(b => b.Id == bikeId);

            if (bike == null)
            {
                return NotFound(new { Message = "Bike not found in the specified bike shop." });
            }

            var bikeDetails = new
            {
                bike.Id,
                bike.Model,
                bike.Brand,
                bike.Description,
                bike.Price,
                bike.RearTravel,
                bike.ForkTravel,
                bike.WaterInBidon,
                BikeShopId = bikeShop.Id,
                bikeShop.Name,
                bike.RideLogSignatures,
                bike.RideLogs
            };

            return Ok(bikeDetails);
        }

        [HttpPost("{shopId}/bikes/{bikeId}/calculateRange")]
        public ActionResult CalculateRange(int shopId, int bikeId)
        {
            var bikeShop = BikeShops.Find(b => b.Id == shopId);
            if (bikeShop == null)
            {
                return NotFound("Bike shop not found.");
            }

            var bike = bikeShop.FindBikeById(bikeId);
            if (bike == null)
            {
                return NotFound("Bike not found in the specified bike shop.");
            }

            Stopwatch stopwatch = new();
            stopwatch.Start();

            int start = shopId + bikeId;
            int end = 300000;
            List<int> primes = CalculatePrimesOptimized(start, end);

            stopwatch.Stop();
            
            _logger.LogInformation("Found {PrimeCount} prime numbers in {ElapsedMs}ms", 
                primes.Count, stopwatch.ElapsedMilliseconds);

            return Ok(new 
            { 
                Message = "Calculated range.",
                PrimesFound = primes.Count,
                ElapsedTimeMs = stopwatch.ElapsedMilliseconds,
                Range = new { Start = start, End = end }
            });
        }
        
        // Optimización usando la Criba de Eratóstenes
        public static List<int> CalculatePrimesOptimized(int start, int end)
        {
            if (end < 2) return new List<int>();
            
            // Criba de Eratóstenes
            bool[] isPrime = new bool[end + 1];
            Array.Fill(isPrime, true);
            isPrime[0] = isPrime[1] = false;
            
            int sqrt = (int)Math.Sqrt(end);
            for (int i = 2; i <= sqrt; i++)
            {
                if (isPrime[i])
                {
                    for (int j = i * i; j <= end; j += i)
                    {
                        isPrime[j] = false;
                    }
                }
            }
            
            // Recolectar primos en el rango
            List<int> primes = new();
            for (int i = Math.Max(2, start); i <= end; i++)
            {
                if (isPrime[i])
                {
                    primes.Add(i);
                }
            }
            
            return primes;
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;
            
            int sqrt = (int)Math.Sqrt(number);
            for (int i = 3; i <= sqrt; i += 2)
            {
                if (number % i == 0) return false;
            }
            return true;
        }
    }
}
