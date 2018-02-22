namespace HVTApp.TestDataGenerator
{
    public static class CloneExtention
    {
        public static void Clone(this object first, object second)
        {
            var props = first.GetType().GetProperties();
            foreach (var propertyInfo in props)
            {
                if (propertyInfo.Name == "Id") continue;
                var value = propertyInfo.GetValue(second);
                propertyInfo.SetValue(first, value);
            }
        }
    }
}