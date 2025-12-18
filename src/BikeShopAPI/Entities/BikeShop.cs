namespace BikeShopAPI.Entities
{
    public class BikeShop
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public string? ImageUrl { get; set; }
        public bool HasDelivery { get; set; }
        public int AddressId { get; set; }
        public virtual List<Bike>? Bikes { get; set; }
        public ShopStatus Status { get; set; }        

        public Bike? FindBikeById(int bikeId)
        {
            return Bikes?.Find(b => b.Id == bikeId);
        }
    }
}
