namespace efcoreApp.Data{

    public class Bootcamp{

        public int BootcampId {get;set;}
        public string? Baslik {get;set;}
        public int? OgretmenId {get;set;}
        public Ogretmen Ogretmen {get;set;} = null!;
        public ICollection<KursKayit> KursKayitlari {get;set;} = new List<KursKayit>();

    }
}