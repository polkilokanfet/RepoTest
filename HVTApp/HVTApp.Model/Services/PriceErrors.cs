using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public class PriceErrors
    {
        private readonly Dictionary<ProductBlock, PriceErrorType> _errors = new Dictionary<ProductBlock, PriceErrorType>();
        private readonly Dictionary<ProductBlock, ProductBlock> _analogs = new Dictionary<ProductBlock, ProductBlock>();

        public void AddError(ProductBlock block, PriceErrorType errorType, ProductBlock analog = null)
        {
            if (_errors.Select(x => x.Key.Id).Contains(block.Id)) return;
            _errors.Add(block, errorType);

            if (_analogs.Select(x => x.Key.Id).Contains(block.Id)) return;
            _analogs.Add(block, analog);
        }

        public string Print()
        {
            return Print(_errors.Select(x => x.Key));
        }

        public string Print(IEnumerable<ProductBlock> blocks)
        {
            var sb = new StringBuilder();
            var errors = _errors.Where(x => blocks.Contains(x.Key)).GroupBy(x => x.Value).ToList();
            foreach (var error in errors)
            {
                switch (error.Key)
                {
                    case PriceErrorType.NoPrice:
                        sb.AppendLine("Блоки без прайса:");
                        break;
                    case PriceErrorType.NoActualPrice:
                        sb.AppendLine("Блоки без актуального прайса:");
                        break;
                    case PriceErrorType.PriceOfAnalog:
                        sb.AppendLine("Блоки с прайсом аналога:");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (error.Key)
                {
                    case PriceErrorType.NoPrice:
                    case PriceErrorType.NoActualPrice:
                        foreach (var er in error)
                        {
                            sb.AppendLine($"{er.Key}");
                        }
                        break;
                    case PriceErrorType.PriceOfAnalog:
                        foreach (var er in error)
                        {
                            sb.AppendLine($"{er.Key} <=> {_analogs[er.Key]}");
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            sb.AppendLine(string.Empty);
            return sb.ToString();
        }
    }
}