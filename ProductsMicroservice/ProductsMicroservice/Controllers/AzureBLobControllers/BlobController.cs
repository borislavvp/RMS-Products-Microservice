using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers.AzureBLobControllers
{
    [Route("api")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private IBlobService _blobService;

        public BlobController(IBlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("blob/upload")]
        public async Task<ActionResult> UploadImage()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await _blobService.UploadBlobAsync(

                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);


            return Ok(result);
        }
        [HttpGet]
        [Route("blob/get/{name}")]

        public ActionResult GetPicture(string name)
        {
            return Ok(_blobService.GetBlobAsync(name));
        }

        [HttpDelete]
        [Route("blob/delete/{name}")]
        public async Task<ActionResult> DeleteImage(string name)
        {
            return Ok(await this._blobService.DeleteBlobAsync(name));
        }
    }
}
