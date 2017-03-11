namespace HVTApp.Model
{
    public class ProductOptional : ProductMain
    {
        /// <summary>
        /// ¬ходит в стоимость основного оборудовани€.
        /// </summary>
        public bool InCoast { get; set; } = true;
    }
}