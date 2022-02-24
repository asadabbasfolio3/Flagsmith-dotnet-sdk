﻿using Example.Settings;
using Flagsmith;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static FlagsmithClient _flagsmithClient;
        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _logger = logger;
            var settings = configuration.GetSection("FlagsmithConfiguration").Get<FlagsmithSettings>();
            _flagsmithClient = new(settings.EnvironmentKey, defaultFlagHandler: defaultFlagHandler);
            static Flag defaultFlagHandler(string featureName)
            {
                if (featureName == "secret_button")
                    return new Flag(name: null, enabled: false, value: JsonConvert.SerializeObject(new { colour = "#b8b8b8" }).ToString());
                else return new Flag() { };
            }
        }
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = HttpContext.Request;
            if (request.Query.Count > 0)
            {
                var Identifier = request.Query["identifier"].ToString();
                var traitKey = request.Query["trait-key"].ToString();
                var traitValue = request.Query["trait-value"].ToString();
                var traitList = new List<Trait> { new Trait(traitKey, traitValue) };
                var flags = await _flagsmithClient.GetIdentityFlags(Identifier, traitList);
                var showButton = await flags.IsFeatureEnabled("secret_button");
                var buttonData = flags.GetFeatureValue("secret_button").Result;
                ViewBag.props = new
                {
                    showButton = showButton,
                    buttonColour = JObject.Parse(buttonData)["colour"].Value<string>(),
                    identifier = Identifier
                };

                return View();
            }
            else
            {

                var flag = await _flagsmithClient.GetEnvironmentFlags();
                var showButton = await flag.IsFeatureEnabled("secret_button");
                var buttonData = flag.GetFeatureValue("secret_button").Result;
                ViewBag.props = new
                {
                    showButton = showButton,
                    buttonColour = JObject.Parse(buttonData)["colour"].Value<string>(),
                    identifier = ""
                };
                return View();
            }
        }
    }
}