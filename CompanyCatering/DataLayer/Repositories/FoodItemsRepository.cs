using CompanyCatering.DataLayer.DBContext;
using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.DataLayer.Repositories
{
    public class FoodItemsRepository : IFoodRepository
    {
        private readonly CompanyCateringDbContext companyCateringDbContext;
        
        public FoodItemsRepository()
        {
            companyCateringDbContext = new CompanyCateringDbContext();
        }

        //cook and admin only!
        public List<FoodItems> ViewAllFoodItems()
        {
            var foods = companyCateringDbContext.FoodItems.ToList();
            return foods;
        }
        
        //cook only!
        public void CreateNewFoodItem(FoodItems foodItems)
        {
            try
            {
                companyCateringDbContext.FoodItems.Add(foodItems);
                companyCateringDbContext.SaveChanges();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        //cook only!
        public void UpdateFoodItems(FoodItems foodItems)
        {
            try
            {
                var foodExists = companyCateringDbContext.FoodItems.Where(p => p.Id == foodItems.Id)
                    .FirstOrDefault();

                if (foodExists == null) 
                {
                    throw new Exception("Food item does not exist!");
                }

                foodExists.Name = foodItems.Name;
                foodExists.Description = foodItems.Description;
                foodExists.Price = foodItems.Price;
                //add for isdeleted if admin

                companyCateringDbContext.FoodItems.Update(foodExists);
                companyCateringDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        //add cook only!
        public void RemoveFoodItems(FoodItems foodItems)
        {
            try
            {
                companyCateringDbContext.Remove(foodItems);
                companyCateringDbContext.SaveChanges();
                Console.WriteLine("Item Removed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex.Message.ToString());
                throw;
            }
            
        }

    }
    
}
