using CrowdFundT2.Core;
using CrowdFundT2.Core.Data;
using CrowdFundT2.Core.Services;
using CrowdFundT2.Core.Services.Interfaces;
using CrowdFundT2.Core.Services.Options;
using System;

namespace CrowdFundT2
{
    public class Program 
    {
        static void Main(string[] args)
        {
            var context_ = new CrowdFundT2DbContext();

            IPackageService package_ = new PackageService(context_);
            IProjectService project_ = new ProjectService(context_);
            IClientService client_ = new ClientService(context_);
            IInvestProjectService invproject_ = new InvestProjectService(context_, package_);


            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Dimitris",
                Lastname = "Pnevmatikos",
                Email = "dpnevmatikos@net.com",
                Phone = "6985647123",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Aris",
                Lastname = "Drakos",
                Email = "aris@net.com",
                Phone = "6987654321",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Dimitris",
                Lastname = "Petrogiannos",
                Email = "dpetrog@net.com",
                Phone = "6942135678",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Orestis",
                Lastname = "Tsioukis",
                Email = "tsouk@net.com",
                Phone = "6912345678",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Dimitris",
                Lastname = "Grevenos",
                Email = "dimgrev@net.com",
                Phone = "6923145678",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Donald",
                Lastname = "Trump",
                Email = "dtrump@net.com",
                Phone = "6914565678",
            });

            client_.CreateClient(new CreateClientOptions()
            {
                Firstname = "Donald",
                Lastname = "Duck",
                Email = "dduck@net.com",
                Phone = "6923142678",
            });


            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 1,
                Title = "Into A Dream",
                Description = "A story-driven atmospheric adventure about love, hopelessness and depression.",
                PostStatusUpdates = "Over 25% in 24 hours!",
                Photos = "https://media.giphy.com/media/keTrwVbKPGXEn6qgHK/giphy.gif",
                Videos = "https://www.youtube.com/watch?v=g2QUrVjyTGs&feature=emb_logo",
                Category = (ProjectCategory)4,
                ProjectCost = 35320.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 2,
                Title = "The Babymaker - Stealth Road eBike With Belt Drive",
                Description = "Leave boring behind. Classic look with modern Ebike tech. Turn heads and crush any hill in style.",
                PostStatusUpdates = "The project team has begun turning their prototype into the final product. Their ability to ship the products may be affected by product development or financial challenges.",
                Photos = "https://c1.iggcdn.com/indiegogo-media-prod-cld/image/upload/f_auto/v1590454593/xcau29sv5mu9gicjhlsk.png",
                Videos = "https://www.youtube.com/watch?v=S9oAVFxaq64&feature=emb_logo",
                Category = (ProjectCategory)7,
                ProjectCost = 63200.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 1,
                Title = "ROIDMI X30 Pro: The Best Mop And Vacuum Cleaner",
                Description = "The project team has begun turning their prototype into the final product. Their ability to ship the products may be affected by product development or financial challenges.",
                PostStatusUpdates = "The project team has begun turning their prototype into the final vaccum.",
                Photos = "https://c3.iggcdn.com/indiegogo-media-prod-cld/image/upload/c_fill,w_695,g_auto,q_auto,dpr_1.0,f_auto,h_460/hgoabeynn2qwxlyljngb",
                Videos = "https://www.youtube.com/watch?v=S9oAVFxaq64&feature=emb_logo",
                Category = (ProjectCategory)7,
                ProjectCost = 3320.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 2,
                Title = "Wounds of Hong Kong 港傷",
                Description = "Portraits of the Hong Kong Protesters and Their Stories",
                PostStatusUpdates = "Wounds of Hong Kong Exhibition.",
                Photos = "https://ksr-ugc.imgix.net/assets/028/917/558/4495375e34a9b317151d4ffc1bdeb81b_original.JPG?ixlib=rb-2.1.0&w=680&fit=max&v=1588308000&auto=format&frame=1&q=92&s=26b4bb3fc85efbe6c50eadeece81b926",
                Videos = "https://www.youtube.com/watch?v=ycXTuZcV4fM",
                Category = (ProjectCategory)1,
                ProjectCost = 8900.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 1,
                Title = "Bowregard is recording our DEBUT ALBUM!",
                Description = "Help 2019 Telluride Bluegrass Festival Band Contest winners Bowregard make their debut album!",
                PostStatusUpdates = "Bowregard Live From the Backyard",
                Photos = "https://ksr-ugc.imgix.net/assets/028/146/599/930207e70c672c8b76fb1480243eff5d_original.jpg?ixlib=rb-2.1.0&w=680&fit=max&v=1582091978&auto=format&frame=1&q=92&s=67700593ffca984c56f7155d42e0b9e6",
                Videos = "https://www.youtube.com/watch?v=O1U5tTm2iH4",
                Category = (ProjectCategory)5,
                ProjectCost = 25620.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 4,
                Title = "I'm Back35. Have a roll already? I'm not jealous.",
                Description = "I'm Back® 35 - a 50's camera that takes digital photos?",
                PostStatusUpdates = "WIN Max add the function of fan silent mode.",
                Photos = "https://indiegogo-media-prod-cld-res.cloudinary.com/image/upload/v1590677888/mtu7oxvnvnrd3ngueifq.jpg",
                Videos = "https://www.youtube.com/watch?v=WgiCn7Rn6lI&feature=emb_logo",
                Category = (ProjectCategory)6,
                ProjectCost = 117000.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 3,
                Title = "The Lost Pages from Zaid Comics!",
                Description = "Stories of the heroes that will come together to write the end of the world. Cover by Simon Bisley!",
                PostStatusUpdates = "FUNDED AND OVER 10K IN 4 DAYS",
                Photos = "https://c1.iggcdn.com/indiegogo-media-prod-cld/image/upload/c_limit,w_695/v1588714308/hhdtxdiazawvczmzgnym.jpg",
                Videos = "https://www.youtube.com/watch?v=MkKkeq3MYwM&feature=emb_logo",
                Category = (ProjectCategory)2,
                ProjectCost = 96720.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 3,
                Title = "PICO: A garden in your palm. Growing is fun again!",
                Description = " Telescopic LED lights, multiple mounts, and self-watering system brings thriving plants everywhere.",
                PostStatusUpdates = "Wow!!! 50% of our goal in only 8 hours",
                Photos = "https://ksr-ugc.imgix.net/assets/029/167/993/c3e92c3d624179bbf818679197d552b5_original.gif?ixlib=rb-2.1.0&w=680&fit=max&v=1590054352&auto=format&gif-q=50&q=92&s=fa2501cbdb2dc6bd68740ecc75ce1aa4%C2%A0",
                Videos = "https://www.youtube.com/watch?v=WgtahT27lUY&feature=emb_logo",
                Category = (ProjectCategory)7,
                ProjectCost = 12720.00M
            });

            project_.CreateProject(new CreateProjectOptions()
            {
                ClientId = 3,
                Title = "All of the Lights",
                Description = "My name is Jeff Ligonde, and I inspire others to imagine new possibilities through visual means.",
                PostStatusUpdates = "FUNDED AND OVER 10K IN 4 DAYS",
                Photos = "https://c1.iggcdn.com/indiegogo-media-prod-cld/image/upload/c_limit,w_695/v1586320760/a3xeipryxucdor5xqrwb.jpg",
                Videos = "https://vimeo.com/404788624",
                Category = (ProjectCategory)3,
                ProjectCost = 3720.00M
            });
            /*
            package_.CreatePackage(new CreatePackageOptions()
            {
                ProjectId = 3,
                Description = "Bicycle perk20",
                Reward = 500
            });


            package_.UpdatePackage(2, new UpdatePackageOptions()
            {
                ProjectId = 3,
                Description = "Bicycle perk4",
                Reward = 550
            });
            */

            /* var invproject1 = invproject_.InvestProject(new InvestProjectOptions()
             {
                 PackageId = 1,
                 ClientId = 2,
                 ProjectId = 2,
             });*/
        }
    }
}
