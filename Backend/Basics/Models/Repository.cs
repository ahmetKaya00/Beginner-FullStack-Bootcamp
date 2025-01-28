namespace Basics.Models{

    public class Repository{

        private static readonly List<Bootcamp> _bootcamp = new();

        static Repository(){
            _bootcamp = new List<Bootcamp>(){
                new Bootcamp() {Id = 1, Title = "Full-Stack Bootcamp", Description = "Full-Stack Bootcamp başlıyor", Image = "1.png", isActive = true,isHome=true},
                new Bootcamp() {Id = 2, Title = "Backend Bootcamp", Description = "Backend Bootcamp başlıyor", Image = "2.png", isActive = true,isHome=false},
                new Bootcamp() {Id = 3, Title = "Game Bootcamp", Description = "Game Bootcamp başlıyor", Image = "3.jpg", isActive = true,isHome=true},
            };
        }

        public static List<Bootcamp> Bootcamps{get{return _bootcamp;}}

        public static Bootcamp? GetById(int? id){
            return _bootcamp.FirstOrDefault(b=>b.Id == id);
        }
    }
}