using Aspose.Imaging;
using Aspose.Imaging.FileFormats.Jpeg;
using Aspose.Imaging.ImageOptions;
using Microsoft.AspNetCore.Mvc;
using RenovaHrEmploymentAPI.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RenovaHrEmploymentAPI.Helpers
{
    class DefaultTemplateHelper
    {
        public static HrRcrtExternalWebsiteInfo GetTemplate(ControllerBase Controller, string PageName)
        {
            var Tpl = DefaultTmpls.Where(t => t.PageName == PageName).FirstOrDefault();
            var CompanyId = ControllerHelpers.GetCompanyId(Controller);
            var Tmpl = new HrRcrtExternalWebsiteInfo
            {
                CompanyId = CompanyId,
                PageName = PageName
            };
            if (Tpl != null)
            {
                Tmpl.Title1English = Tpl.Title1English;
                Tmpl.Title1Spanish = Tpl.Title1Spanish;
                Tmpl.Text1English = Tpl.Text1English;
                Tmpl.Text1Spanish = Tpl.Text1Spanish;
            }
            return Tmpl;

        }

        private static IList<Tpl> DefaultTmpls = new List<Tpl>
        {
            new Tpl
            {
                PageName = "Registration",
                Text1English = "Dear User, please complete complete your registration just clicking the following address <a href=\"[ACTIVATIONLINK]\">[ACTIVATIONLINK]</a>",
                Text1Spanish = "Estimado usuario, por favor complete el registro haciendo click en el siguiente enlace <a href=\"[ACTIVATIONLINK]\">[ACTIVATIONLINK]</a>"
            },
            new Tpl
            {
                PageName = "ResetPassword",
                Text1English = "Dear User, please complete your password reset just clicking the following address <a href=\"[ACTIVATIONLINK]\">[ACTIVATIONLINK]</a>",
                Text1Spanish = "Estimado usuario, por favor reestablezca su contraseña haciendo click en el siguiente enlace <a href=\"[ACTIVATIONLINK]\">[ACTIVATIONLINK]</a>"
            },
            new Tpl
            {
                PageName = "HrPositionRequest",
                Text1English = @"A new job application has been received.<br><br>

Applicant: [FIRST_NAME] [LAST_NAME1]<br>
Position: [POSITION_DESCRIPTION] < br >
< br />
< br />
Please verify in the administration area.
<br>
<br />
<strong>Human Resources Department</strong> <br>
",
                Text1Spanish = @"Se ha recibido una nueva solicitud de empleo.<br><br>
Solicitante: [FIRST_NAME] [LAST_NAME1]<br>
Posición: [POSITION_DESCRIPTION]<br>
<br />
Por favor verifique en el área de administración. 
<br />
<br />
<strong>Departamento de Recursos Humanos</strong><br>
",
                Title1English = "New Job Application for [POSITION_DESCRIPTION]",
                Title1Spanish = "Nueva solicitud de empleo para el puesto [POSITION_DESCRIPTION]"
            },
            new Tpl
            {
                PageName = "UserPositionRequest",
                Text1English = @"Thanks for your interest in working in Supermax.<br>We receive your job application.<br>We will contact you soon.<br><br>
<br />
<strong>Departamento de Recursos Humanos</strong><br>
",
                Text1Spanish = @"Gracias por su interés en trabajar en SuperMax.<br>
        Recibimos su solicitud de empleo.<br>Próximamente le contactamos.<br><br>
<br />
<strong>Departamento de Recursos Humanos</strong><br>
",
                Title1English = "Your job application has been received",
                Title1Spanish = "Su Solicitud de Empleo Ha Sido Recibida"
            }

        };

        private class Tpl
        {

            public string PageName { get; set; }
            public string Title1Spanish { get; set; }
            public string Title1English { get; set; }
            public string Text1Spanish { get; set; }
            public string Text1English { get; set; }
        }
    }
}
