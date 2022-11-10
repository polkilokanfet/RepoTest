using System.Collections.Generic;

namespace HVTApp.Infrastructure
{
    public static class BaseEntityExtensions
    {
        /// <summary>
        /// Вернуть простую хэш-сумму всех элементов (не чувствительную к порядку элеменов)
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static int GetHashSum(this IEnumerable<IBaseEntity> entities)
        {
            unchecked
            {
                int result = 0;
                foreach (var entity in entities)
                {
                    result += entity.GetHashCode();
                }

                return result;
            }
        }

    }
}