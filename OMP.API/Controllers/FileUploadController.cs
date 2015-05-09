using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.ServiceModel.Channels;
using OMP.API.Hubs;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using OMP.API.Models;
using OMP.Service.Interface;
using OMP.Service.ViewModel;

namespace OMP.API.Controllers
{
    [RoutePrefix("api/File")]
    public class FileUploadController : ApiController
    {
        private IUserProfileService userProfileService;
        private IProductService productService;
        private ProductImageViewModel productImageVM;

        public FileUploadController(IUserProfileService userProfileService, IProductService productService) 
        {
            this.userProfileService = userProfileService;
            this.productService = productService;
        }
        // Declare HubContext
        protected readonly Lazy<IHubContext> productNotificationHub = 
            new Lazy<IHubContext>(() => GlobalHost.ConnectionManager.GetHubContext<ProductHub>());

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ProfileUpload")]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider("~/Content/UserProfileUpload");
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            string uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName).ToString();

            string[] fileSplit = originalFileName.Split('.');

            string fileType = fileSplit[fileSplit.Length - 1];

            string destinationFile = uploadedFileInfo + "." + fileType;

            System.IO.File.Move(uploadedFileInfo, destinationFile);

            string uploadedFileName = destinationFile.Split('\\')[destinationFile.Split('\\').Length - 1];

            userProfileService.UpdateUserImage(User.Identity.Name, uploadedFileName);

            var fileUploadObj = GetFormData<UploadDataModel>(result);

            var returnData = uploadedFileName;
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductImageUpload")]
        public async Task<HttpResponseMessage> UploadProductImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider("~/Content/ProductImageUpload");
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var originalFileName = GetDeserializedFileName(result.FileData.First());

            string uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName).ToString();

            string[] fileSplit = originalFileName.Split('.');

            string fileType = fileSplit[fileSplit.Length - 1];

            string destinationFile = uploadedFileInfo + "." + fileType;

            System.IO.File.Move(uploadedFileInfo, destinationFile);

            string uploadedFileName = destinationFile.Split('\\')[destinationFile.Split('\\').Length - 1];


            var fileUploadObj = (UploadDataModelForProduct)GetFormData<UploadDataModelForProduct>(result);

            productImageVM = new ProductImageViewModel
            {
                ProductID = fileUploadObj.productID,
                ImageUrl = uploadedFileName
            };

            await productService.CreateProductImage(productImageVM);

            productService.UpdateProductSummaryImage(fileUploadObj.productID, uploadedFileName);

            var returnData = productService.GetProductImageByProductID(fileUploadObj.productID);
            return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uploadFolder"></param>
        /// <returns></returns>
        private MultipartFormDataStreamProvider GetMultipartProvider(string uploadFolder)
        {
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        private object GetFormData<T>(MultipartFormDataStreamProvider result)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(0).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }
    }
    
}
