using System;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class RoundUpModule
    {
        private double _roundUpAccuracy = 5000;

        /// <summary>
        /// Коэффициент округления
        /// </summary>
        public double RoundUpAccuracy
        {
            get => _roundUpAccuracy;
            set
            {
                if (value <= 0) return;
                _roundUpAccuracy = value;
            }
        }

        public double RoundUp(double origin)
        {
            return Math.Ceiling(origin / RoundUpAccuracy) * RoundUpAccuracy;
        }
    }
}