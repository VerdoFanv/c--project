using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uts {
  class Food : Menu {
    private bool fried, broth;

    public Food(string name, double price, bool fried = false, bool broth = false) : base(name, price) {
      this.fried = fried;
      this.broth = broth;
    }

    public bool getFried() {
      return this.fried;
    }

    public bool getBroth() {
      return this.broth;
    }
  }
}
