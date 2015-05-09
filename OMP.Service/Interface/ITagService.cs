using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Interface
{
    public interface ITagService
    {
        int CreateTag(TagViewModel tagViewModel);
        TagViewModel GetTagByID(int tagID);
    }
}
