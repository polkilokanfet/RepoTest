using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService.ProductGeneration
{
    public class BlockGeneration
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Parameter[] _allParameters;

        private IEnumerable<PathToOrigin> AllPaths => _allParameters.SelectMany(parameter => parameter.Paths());

        //полные пути параметров
        private IEnumerable<PathToOrigin> FullPaths => AllPaths.Where(x => AllPaths.Count(p => x.Parameters.AllContainsIn(p.Parameters)) == 1);

        public BlockGeneration(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _allParameters = unitOfWork.Repository<Parameter>().GetAll();
        }

        /// <summary>
        /// Возвращает все полные пути параметров.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PathToOrigin> GetFullPaths()
        {
            return AllPaths.Where(pathToOrigin => AllPaths.Count(p => pathToOrigin.Parameters.AllContainsIn(p.Parameters)) == 1);
        }

        //всё, что ниже, слишком мудрено и нихрена не работает

        public IEnumerable<ProductBlock> GetAllBlocks()
        {
            var blocks = new List<ProductBlock>();
            foreach (var path in GetFullPaths())
            {
                blocks.AddRange(GetBlocks(new List<PathToOrigin> {path}, GetFullPaths().ToList()));
            }
            return blocks.Distinct(new BlockComparer());
        }

        private IEnumerable<ProductBlock> GetBlocks(List<PathToOrigin> paths, List<PathToOrigin> potentialPaths)
        {
            if (!potentialPaths.Any())
            {
                yield return new ProductBlock { Parameters = paths.SelectMany(x => x.Parameters).ToList() };
                yield break;
            }

            //исключаем пути без обязательных параметров
            var requiredParameters = paths.SelectMany(x => x.Relations).SelectMany(x => x.RequiredParameters).Distinct().ToList();
            if (requiredParameters.Any())
                potentialPaths = potentialPaths.Where(x => requiredParameters.AllContainsIn(x.Parameters)).ToList();

            //исключаем пути с одной группой на конце
            var groups = paths.Select(x => x.Parameters.First().ParameterGroup).Distinct().ToList();
            potentialPaths = potentialPaths.Where(x => x.Parameters.Select(p => p.ParameterGroup).Intersect(groups).Any()).ToList();
            if (potentialPaths.Any())
            {
                foreach (var potentialPath in potentialPaths)
                {
                    var blocks = GetBlocks(paths.Concat(new []{potentialPath}).ToList(), potentialPaths.Except(new []{potentialPath}).ToList());
                    foreach (var productBlock in blocks)
                    {
                        yield return productBlock;
                    }
                }
            }

            var blocks2 = GetBlocks(paths, new List<PathToOrigin>());
            foreach (var productBlock in blocks2)
            {
                yield return productBlock;
            }
        }
    }

    public class BlockComparer : IEqualityComparer<ProductBlock>
    {
        public bool Equals(ProductBlock x, ProductBlock y)
        {
            return x.Parameters.MembersAreSame(y.Parameters);
        }

        public int GetHashCode(ProductBlock obj)
        {
            return 0;
        }
    }
}
