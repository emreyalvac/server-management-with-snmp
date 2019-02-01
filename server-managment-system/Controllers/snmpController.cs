using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Management;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using server_managment_system.Repostories.Cpu;

namespace server_managment_system.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ICpu _cpu;

        public ValuesController(ICpu cpu)
        {
            _cpu = cpu;
        }

        [HttpGet, Route("snmp")]
        public string WMI([FromQuery] string identifier)
        {
            ISnmpData data = null,
                data2 = null;
            GetNextRequestMessage message = new GetNextRequestMessage(0,
                VersionCode.V1,
                new OctetString("public"),
                //new List<Variable> {new Variable(new ObjectIdentifier("1.3.6.1.2.1.25.1.6"))});
                new List<Variable> {new Variable(new ObjectIdentifier(identifier))});
            ISnmpMessage response = message.GetResponse(60000, new IPEndPoint(IPAddress.Any, 161));

            string s = JsonConvert.SerializeObject(response.Pdu().Variables);

            foreach (var a in response.Pdu().Variables)
            {
                data2 = a.Data;
            }

            var result = Messenger.Get(VersionCode.V2,
                new IPEndPoint(IPAddress.Any, 161),
                new OctetString("public"),
                new List<Variable> {new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.3"))},
                60000);
            s += JsonConvert.SerializeObject(result);
            return s;
        }

        public JsonResult getSingleInformation()
        {
            ISnmpData data = null;
            var result = Messenger.Get(VersionCode.V2,
                new IPEndPoint(IPAddress.Any, 161),
                new OctetString("public"),
                new List<Variable> {new Variable(new ObjectIdentifier(".1.3.6.1.2.1.1.3"))},
                60000);
            foreach (var a in result)
            {
                data = a.Data;
            }


            return new JsonResult(new
            {
                SystemUpTime = data.ToString(),
            });
        }
    }
}