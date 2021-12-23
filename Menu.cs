using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uts {
  public abstract class Menu {
    public string name { get; }
    public double price { get; }
    private int orderCount = 0;

    public Menu(string name, double price) {
      this.name = name;
      this.price = price;
    }

    public double getTotalPrice() {
      return this.orderCount * this.price;
    }

    public int getOrderCount() {
      return this.orderCount;
    }

    public void setOrderCount(int orderCount) {
      this.orderCount = orderCount;
    }
  }
}
