using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uts {
  class Drink : Menu {
    private bool useIce;

    public Drink(string name, double price, bool useIce = false) : base(name, price) {
      this.useIce = useIce;
    }

    public bool getUseIce() {
      return this.useIce;
    }
  }
}
