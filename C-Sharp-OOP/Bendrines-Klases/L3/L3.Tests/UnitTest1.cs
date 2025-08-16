using Xunit;
using L3.MyClasses;

namespace L3.Tests
{

    public class TripLinkListTests
    {
        [Fact]
        public void TwoStringEqualLength()
        {
            Trip ob1 = new Trip("A", "B", TimeSpan.Parse("15:00"), "C", TimeSpan.Parse("18:00"), 4);
            Trip ob2 = new Trip("Ab", "Bcd", TimeSpan.Parse("11:00"), "ccc", TimeSpan.Parse("15:00"), 9);

            Assert.Equal(ob1.ToString().Length, ob2.ToString().Length);
        }
    
    [Fact]
       
    }

  