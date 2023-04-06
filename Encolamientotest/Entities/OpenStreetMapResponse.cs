namespace Encolamientotest.Entities
{
    public class OpenStreetMapResponse
    {
        public List<Route> routes { get; set; }
    }

    public class Route
    {
        public double distance { get; set; }
    }

    public class OpenStreetMapGeocodeResult
    {
        public List<OpenStreetMapGeocodeAddress> place { get; set; }
    }

    

public class OpenStreetMapGeocodeAddress
{
    public int place_id { get; set; }
    public string licence { get; set; }
    public string osm_type { get; set; }
    public int osm_id { get; set; }
    public string[] boundingbox { get; set; }
    public string lat { get; set; }
    public string lon { get; set; }
    public string display_name { get; set; }
    public string _class { get; set; }
    public string type { get; set; }
    public float importance { get; set; }
    public Address address { get; set; }
}

public class Address
{
    public string road { get; set; }
    public string neighbourhood { get; set; }
    public string suburb { get; set; }
    public string city { get; set; }
    public string ISO31662lvl8 { get; set; }
    public string state_district { get; set; }
    public string state { get; set; }
    public string ISO31662lvl4 { get; set; }
    public string postcode { get; set; }
    public string country { get; set; }
    public string country_code { get; set; }
}
}

