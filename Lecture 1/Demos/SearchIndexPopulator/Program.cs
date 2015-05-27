using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchIndexPopulator
{
	class Program
	{
		private static readonly IEnumerable<CompanyInfo> companies = new[]
		{
			new CompanyInfo { Id = "01", Name = "Fliiby", Description = "Fliiby is digital content publishing platform that can help you monetize your music, videos, photos, ebooks and files.", Website = "http://fliiby.com" },
			new CompanyInfo { Id = "02", Name = "Prizmos", Description = "Mobile app that allows any driver to customize how their car behaves (auto headlighs, door locks, A/C, displays, etc.) via a Bluetooth device that plugs into the vehicle’s diagnostic port. One can also run advanced dealer-level diagnostics to check the health of all vehicle systems.", Website = "http://www.prizmos.com" },
			new CompanyInfo { Id = "03", Name = "Cinexio", Description = "Cinexio is a mobile and web app that offers users a quick and easy way to buy cinema tickets via a social movie-search experience. It also offers a variety of details including summaries, ratings, comments and a personal profile that allows users to create watchlists and follow their booking history.", Website = "http://cinexio.com" },
			new CompanyInfo { Id = "04", Name = "Imagga", Description = "Businesses don’t have convenient ways to understand, organize and monetize the huge amounts of user-generated images and as result waste a lot of resources and miss opportunities. Our solution that fixes this “image blindness”, is a set of APIs for automated image categorization, annotation and metadata extraction, done via machine learning and proprietary image recognition algorithms. Imagga APIs can be used either separately or in a bundle.", Website = "http://imagga.com" },
			new CompanyInfo { Id = "05", Name = "Ucha.se", Description = "Ucha.se is an online education platform with video lessons and exercises, which pupils and teachers use to prepare for school. The platform currently (July, 2014) offers over 3200 videos (+150 new videos every month), watched more than 7 000 000 times. Since its launch in 2012, www.ucha.se has won 20+ awards for contribution in the modernization of education and distinguished as best educational website in Bulgaria for 2012 and 2013. The platform currently has 220 000 registered users – pupils, teachers, students and parents.", Website = "http://ucha.se" },
			new CompanyInfo { Id = "06", Name = "BitLendingClub", Description = "BitLendingClub is an international Bitcoin crowd-lending platform. It’s online marketplace here people meet to lend and borrow bitcoins. The lenders compete each other in order to assess the risk of each loan and we let the free market provide the most competitive rates. BitLendingClub is the first ever company to utilize smart property, made possible by the block-chain protocol, in order to eliminate the risk of borrower malfeasance.", Website = "http://bitlendingclub.com" },
			new CompanyInfo { Id = "07", Name = "Bgmenu / Hello Hungry", Description = "GMENU is the fastest and easiest way to order food wherever you are. With just a few clicks you can choose and order from the largest and ever-growing portfolio of restaurants in the country and the region, over 250 restaurants across the country, which increase in number every day. Whether your favorite food is Pizza, Chinese, Indian, Bulgarian, Italian or sushi, in BGMENU you’ll find the largest selection of restaurants. Besides delivery, you can order on the go and get your food straight from the restaurant with the new service Take Out. BGmenu is the brand in Bulgaria, HELLOHUNGRY is the brand for the neighbouring markets.", Website = "http://bgmenu.com" },
			new CompanyInfo { Id = "08", Name = "Intuitics", Description = "Data scientists in large companies are frequently asked by business users to rerun analyses with different parameters and data. This wastes time and slows down decision making. Intuitics solves this problem by empowering self-service advanced analytics. We make it quick and easy for data scientists to build interactive analytics web applications that non-specialists use to generate and share their own insights. This speeds up decision making, makes it more data-driven, and saves time for the data team. Intuitics also makes it dead simple to integrate analytics models with existing systems. It comes in both on-premise and SaaS versions.", Website = "http://intuitics.com" },
			new CompanyInfo { Id = "09", Name = "Flyver", Description = "Drones today have limited set of functionality. Similar to phones in mid 2000, they offer limited or no programming opportunity for developers.For real innovation to happen we need SmartDrones and environment where developers could build up on previous work.Flyver is up to that task. Flyver apps can transform a regular drone into anything from crazy flying toys to serious real life problem - solver for numerous cases – monitoring powerlines, private property surveillance, rescue missions assistance.", Website = "http://flyver.co" },
			new CompanyInfo { Id = "10", Name = "Jumpido", Description = "Jumpido is an exciting series of educational games for primary school. It combines natural body exercises with engaging math problems to make learning a truly enjoyable experience.", Website = "http://jumpido.com" },
			new CompanyInfo { Id = "11", Name = "Clusterize", Description = "Clusterize is the easiest way to see where others are going — and why you should be there, too. It’s an Android and iOS app that acts as your personal guide for a better social life, with friends who matter to you and places that rock. Ever missed an awesome event, an epic party, or a cool movie because it got lost in the sea of notifications and invites out there? You no longer have to browse endless lists of boring events on Facebook to spot the ones that you really like. Instead, just open up Clusterize, log in with your Facebook account, and find all your events categorized automatically and sorted by location. Choose your favourite category, tap on an event, and instantly see who’s going there and how close to you it is.", Website = "http://clusterize.co" },
			new CompanyInfo { Id = "12", Name = "AdTapsy", Description = "AdTapsy is innovative and easy-to-use web platform that helps mobile application publishers to drive maximized revenue from in-app advertising. With AdTapsy’s SDK application developers and publishers can integrate and manage more than one ad network in their applications. AdTapsy is auto-prioritizing and rotating those ad networks in order to bring maximum revenue for application publishers out of their ad inventory.", Website = "http://addtapsy.com" },
			new CompanyInfo { Id = "13", Name = "Jobio", Description = "The job searching and offering process is quite static and not effective enough. It is CV-based, with emphasis on on experience rather than on skills. It is quite time and resource consuming without leading to desiredresults. Jobio offers a solution through a gamified job platform based on simulational games. This will help people to show their skills, while companies will save time and resources and optimize the recruitment process. It is a match-making both for employees and employers.", Website = "http://jobio.me" },
			new CompanyInfo { Id = "14", Name = "IQ friends", Description = "IQ Friends provides brain training that is fun and social. The brain training is available on both web, as well as across many mobile plattforms. The brain training is based on the research of neuroscientists and trains these 6 areas: Reaction Time, Logic, Short-term Memory, Flexibility, Arithmetics, Visual Perception.", Website = "http://iqfriends.com" },
			new CompanyInfo { Id = "15", Name = "Perpetto", Description = "Perpetto is a ready-to-use recommendation engine for small and medium online stores. We help the store display personalised product recommendations to each individual user without any upfront cost, heavy configuration or domain knowledge required. Perpetto also provides the store owner with real-time insights how the recommendations perform and impact core business metrics such as user engagement, product conversion rate and revenue.", Website = "http://perpetto.com" },
		};

		static void Main(string[] args)
		{
			SearchHelper<CompanyInfo>.CreateIndex();
			SearchHelper<CompanyInfo>.PopulateData(companies);
			while (true)
			{
				Console.WriteLine("----------------------------------------------------------");
				Console.Write("Input query:");
				var query = Console.ReadLine();
				var searchResults = SearchHelper<CompanyInfo>.Search(query);
				if (searchResults.Any())
				{
					foreach (var result in searchResults)
					{
						Console.WriteLine("Name: {0}, Website: {1}", result.Name, result.Website);
					}
				}
				else
				{
					Console.WriteLine("No results :(");
				}
			}
		}
	}
}
