using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcessLayer.DTO
{
    public class EmailDataWithAttachment : EmailDataSMTP
    {
        public IFormFileCollection EmailAttachments { get; set; }
    }
}