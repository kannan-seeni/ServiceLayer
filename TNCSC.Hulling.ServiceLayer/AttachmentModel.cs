using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TNCSC.Hulling.ServiceLayer
{
    public class AttachmentModel

    {
        #region Properties
    
        public string FileType { get; set; }
        
        public string FileName { get; set; }
      
        public byte[] FileContents { get; set; }
        
        public bool Status { get; set; }
       

        #endregion
    }
}
