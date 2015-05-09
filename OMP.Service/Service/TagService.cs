using OMP.Domain.Model;
using OMP.Domain.Repositories;
using OMP.Service.Interface;
using OMP.Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMP.Service.Service
{
    public class TagService : ITagService
    {
        private UnitOfWork unitOfWork;
        private Tag tag;

        public TagService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        public int CreateTag(TagViewModel tagViewModel)
        {
            tag = new Tag
            {
                TagName = tagViewModel.TagName
            };

            unitOfWork.TagRepository.Insert(tag);
            unitOfWork.Save();

            return tag.TagID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        private Tag GetTagEntityByID(int tagID)
        {
            tag = (from s in unitOfWork.TagRepository.Get()
                        where s.TagID == tagID
                        select s).SingleOrDefault();

            return tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public TagViewModel GetTagByID(int tagID)
        {
            var tag = (from s in unitOfWork.TagRepository.Get()
                       where s.TagID == tagID
                       select new TagViewModel
                                 {
                                     TagID = s.TagID,
                                     TagName = s.TagName
                                 }).SingleOrDefault();
            return tag;
        }
    }
}
