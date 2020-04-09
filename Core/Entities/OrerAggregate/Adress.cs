namespace Core.Entities.OrerAggregate
{
  public class Adress
  {
    public Adress()
    {
    }

    public Adress(string firstName, string lastName, string street, string city, string state, string zipcode)
    {
      FirstName = firstName;
      LastName = lastName;
      Street = street;
      City = city;
      State = state;
      Zipcode = zipcode;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zipcode { get; set; }

  }
}