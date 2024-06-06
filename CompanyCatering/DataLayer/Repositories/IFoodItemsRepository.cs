using CompanyCatering.DataLayer.Entities;

namespace CompanyCatering.DataLayer.Repositories
{
    public interface IFoodRepository
    {
        public List<FoodItems> ViewAllFoodItems();
        public void CreateNewFoodItem(FoodItems FoodItems);
        public void UpdateFoodItems(FoodItems FoodItems);
        public void RemoveFoodItems(FoodItems FoodItems);
        
    }
}
