using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uts {
  class Root {
    // resto profile (note: boleh diubah valuenya)
    private static double initialDiscountPrice = 100000;
    private static string storeName = "Fernando Fanjaya";
    private static double discountPriceInPercent = 30;

    // jangan mengubah code dibawah ini !
    private static int countFoodKey = 0, countDrinkKey = 0;
    private static Food[] foodPil = Array.Empty<Food>();
    private static Drink[] drinkPil = Array.Empty<Drink>();
    private static Invoices invoices = new Invoices();
    private static bool session = false;

    static void Main(string[] args) {
      Console.WriteLine($"{equalBarrier()} Dapatkan diskon {discountPriceInPercent}%, setiap min belanja Rp {initialDiscountPrice.ToString("#,###")} {equalBarrier()}");

      showMenu();
    }

    private static void showMenu() {
      Root mainClass = new Root();

      // daftar menu makanan
      Food pecelAyam = new Food("PECEL AYAM", 15000, true);
      Food pecelLele = new Food("PECEL LELE", 15000, true);
      Food nasiGorengKambing = new Food("NASI GORENG KAMBING", 22000, true);
      Food nasiGorengSosis = new Food("NASI GORENG SOSIS", 15000, true);

      // daftar menu minuman
      Drink esTehManis = new Drink("ES TEH MANIS", 4000, true);
      Drink esJeruk = new Drink("ES JERUK", 5000, true);
      Drink tehManisHangat = new Drink("TEH MANIS HANGAT", 2000);
      Drink jerukHangat = new Drink("JERUK HANGAT", 3000);
      Drink botolMineral = new Drink("BOTOL MINERAL", 4000);

      // daftarkan menu untuk di proses
      Food[] menus1 = { pecelAyam, pecelLele, nasiGorengKambing, nasiGorengSosis };
      Drink[] menus2 = { esTehManis, esJeruk, tehManisHangat, jerukHangat, botolMineral };
      double totalPrice = 0;

      // angka initial jumlah menu
      int num = 2;
      Console.WriteLine($"{equalBarrier()} Restorant {storeName} {equalBarrier()}");
      Console.WriteLine("1. Keluar / Exit\n");

      foreach (var menu in menus1) {
        if (menu.getFried()) {
          Console.WriteLine($"{num}. Nama makanan: {menu.name}\nharga: Rp  {menu.price.ToString("#,###")}\nDigoreng? Iya\nBerkuah? Tidak\n");
          num++;
        } else {
          if (menu.getBroth()) {
            Console.WriteLine($"{num}. Nama makanan: {menu.name}\nharga: Rp  {menu.price.ToString("#,###")}\nDigoreng? Tidak\nBerkuah? Iya\n");
            num++;
          } else {
            Console.WriteLine($"{num}. Nama makanan: {menu.name}\nharga: Rp  {menu.price.ToString("#,###")}\nDigoreng? Tidak\nBerkuah? Tidak\n");
            num++;
          }
        }
      }

      foreach (var menu in menus2) {
        if (menu.getUseIce()) {
          Console.WriteLine($"{num}. Nama minuman: {menu.name}\nharga: Rp  {menu.price.ToString("#,###")}\nPakai Es? Iya\n");
          num++;
        } else {
          Console.WriteLine($"{num}. Nama minuman: {menu.name}\nharga: Rp  {menu.price.ToString("#,###")}\nPakai Es? Tidak\n");
          num++;
        }
      }
      
      mainClass.questions(menus1, menus2, totalPrice);
    }

    private void questions(Food[] menus1, Drink[] menus2, double totalPrice) {
      string answer;
      Root mainClass = new Root();

      if (session) {
        Console.Write("Apakah anda ingin melanjutkan pembelian? ya[y] / tidak[t]: ");
        answer = Convert.ToString(Console.ReadLine());

        if (answer == "ya" || answer == "y") {
          mainClass.purchasing(menus1, menus2, totalPrice);
        } else if (answer == "tidak" || answer == "t") {
          mainClass.purchasing(menus1, menus2, totalPrice, 78998654);
        } else {
          Console.WriteLine("Maaf saya tidak mengerti yang anda maksud...\n");
          mainClass.questions(menus1, menus2, totalPrice);
        }
      } else {
        session = true;
        Console.Write("Apakah anda ingin memulai pembelanjaan? ya[y] / tidak[t]: ");
        answer = Convert.ToString(Console.ReadLine());

        if (answer == "ya" || answer == "y") {
          mainClass.purchasing(menus1, menus2, totalPrice);
        } else if (answer == "tidak" || answer == "t") {
          Console.WriteLine("Terima kasih sudah lihat2 ...");
          Console.ReadKey(true);
          Environment.Exit(0);
        } else {
          Console.WriteLine("Maaf saya tidak mengerti yang anda maksud...\n");
          mainClass.questions(menus1, menus2, totalPrice);
        }
      }
    }

    private void purchasing(Food[] menus1, Drink[] menus2, double totalPrice, int caseKey = 0) {
      int menuPil;
      string invoicesText = "";

      if (caseKey == 0) {
        Console.Write("Masukkan menu pilihan: ");
      }

      menuPil = (caseKey == 0) ? Convert.ToInt32(Console.ReadLine()) : caseKey;

      switch (menuPil) {
        case 78998654: {
          if (foodPil.Length > 0) {
            foreach (var food in foodPil) {
              totalPrice += food.getTotalPrice();
            }

            if (drinkPil.Length > 0) {
              foreach (var drink in drinkPil) {
                totalPrice += drink.getTotalPrice();
              }
            }
          } else if (drinkPil.Length > 0) {
            foreach (var drink in drinkPil) {
              totalPrice += drink.getTotalPrice();
            }
          } else {
            Console.WriteLine("Tidak ada yang dibeli...\n");
            questions(menus1, menus2, totalPrice);
          }

          double discount = discountPriceInPercent / 100;

          string invoiceId = invoices.idGenerator();
          Console.WriteLine($"\n{equalBarrier()} Menghitung total pembelian {equalBarrier()} \n");

          if (totalPrice > initialDiscountPrice) {
            invoicesText += $"\nSelamat anda mendapatkan diskon sebesar {totalPrice * discount:#,###}\n";
            invoicesText += "\nMenu yang dibeli\n\n";

            foreach (var food in foodPil) {
              invoicesText += $"{food.getOrderCount()} {food.name}\t\t\t\t{food.price.ToString("#,###")}\n";
            }

            foreach (var drink in drinkPil) {
              invoicesText += $"{drink.getOrderCount()} {drink.name}\t\t\t\t{drink.price.ToString("#,###")}\n";
            }

            invoicesText += $"\n{equalBarrier()}{equalBarrier()}{equalBarrier()}";

            invoicesText += $"\nSubtotal:\t\t\t\t{totalPrice.ToString("#,###")}";
            invoicesText += $"\nPotongan harga: \t\t\t-{totalPrice * discount:#,###}";
            totalPrice = totalPrice - (totalPrice * discount);
            invoicesText += $"\nTotal harga yang harus dibayar:\t\t{totalPrice.ToString("#,###")}";

            invoicesText += $"\n{equalBarrier()}{equalBarrier()}{equalBarrier()}\n";
            Console.WriteLine(invoicesText);

            invoicesText += processPayment(totalPrice);

            invoicesText += $"\n{equalBarrier()} {DateTime.Now.ToString("dddd, dd MMM yyyy HH:mm:ss")} {equalBarrier()}\n";
            invoicesText += $"{equalBarrier()} {invoiceId} {equalBarrier()}\n";
            invoicesText += "Terima kasih sudah memesan...";
            // end
            Console.WriteLine($"\n{equalBarrier()} {DateTime.Now.ToString("dddd, dd MMM yyyy HH:mm:ss")} {equalBarrier()}");
            Console.WriteLine($"{equalBarrier()} {invoiceId} {equalBarrier()}");
            Console.WriteLine("Terima kasih sudah memesan...");

            // simpan ke struk belanja
            invoices.create(invoicesText, invoiceId);

            Console.ReadKey(true);
            Environment.Exit(0);
          } else {
            invoicesText += "\nMenu yang dibeli\n\n";
            foreach (var food in foodPil) {
              invoicesText += $"{food.getOrderCount()} {food.name}\t\t\t\t{food.price.ToString("#,###")}\n";
            }

            foreach (var drink in drinkPil) {
              invoicesText += $"{drink.getOrderCount()} {drink.name}\t\t\t\t{drink.price.ToString("#,###")}\n";
            }

            invoicesText += $"\nTotal harga yang harus dibayar:\t\t{totalPrice.ToString("#,###")}\n";
            invoicesText += $"\n{equalBarrier()}{equalBarrier()}{equalBarrier()}\n";
            Console.WriteLine(invoicesText);

            invoicesText += processPayment(totalPrice);

            invoicesText += $"\n{equalBarrier()} {DateTime.Now.ToString("dddd, dd MMM yyyy HH:mm:ss")} {equalBarrier()}\n";
            invoicesText += $"{equalBarrier()} {invoiceId} {equalBarrier()}\n";
            invoicesText += "Terima kasih sudah memesan...";
            // end
            Console.WriteLine($"\n{equalBarrier()} {DateTime.Now.ToString("dddd, dd MMM yyyy HH:mm:ss")} {equalBarrier()}");
            Console.WriteLine($"{equalBarrier()} {invoiceId} {equalBarrier()}");
            Console.WriteLine("Terima kasih sudah memesan...");

            // simpan ke struk belanja
            invoices.create(invoicesText, invoiceId);

            Console.ReadKey(true);
            Environment.Exit(0);
          }

          break;
        }
        case 1: {
          if (foodPil.Length > 0) {
            break;
          } else {
            Console.WriteLine("Terima kasih sudah memesan...");
            Environment.Exit(0);
            break;
          }
        }
        // makanan
        case 2: {
          askOrderFoodCounts(menus1, 0);
          break;
        }
        case 3: {
          askOrderFoodCounts(menus1, 1);
          break;
        }
        case 4: {
          askOrderFoodCounts(menus1, 2);
          break;
        }
        case 5: {
          askOrderFoodCounts(menus1, 3);
          break;
        }
        // minuman
        case 6: {
          askOrderDrinkCounts(menus2, 0);
          break;
        }
        case 7: {
          askOrderDrinkCounts(menus2, 1);
          break;
        }
        case 8: {
          askOrderDrinkCounts(menus2, 2);
          break;
        }
        case 9: {
          askOrderDrinkCounts(menus2, 3);
          break;
        }
        case 10: {
          askOrderDrinkCounts(menus2, 4);
          break;
        }
        // default
        default: {
          Console.WriteLine("Maaf menu yang anda masukkan tidak ada...");
          questions(menus1, menus2, totalPrice);
          break;
        }
      }

      questions(menus1, menus2, totalPrice);
    }

    private static string processPayment(double totalPrice) {
      string invoicesText = "";
      int payment;

      do {
        Console.Write("Masukkan uang pembayaran: ");
        payment = int.Parse(Console.ReadLine());
        Console.WriteLine(equalBarrier());

        if (payment >= totalPrice) {
          invoicesText += $"Uang bayar\t{payment.ToString("#,###")}\n\n";
          invoicesText += $"Kembalian\t{((payment - totalPrice) > 0 ? (payment - totalPrice).ToString("#,###") : payment - totalPrice)}\n\n";

          Console.WriteLine(invoicesText);
        } else {
          Console.WriteLine($"Maaf uang yang anda masukkan tidak mencukupi, -{totalPrice - payment:#,###}");
        }
      } while (payment < totalPrice);

      return invoicesText;
    }

    private static void askOrderFoodCounts(Food[] menus, int index) {
      var menu = menus[index];
      int count;

      Console.Write("Masukkan jumlah yang ingin dibeli: ");
      count = Convert.ToInt32(Console.ReadLine());
      menu.setOrderCount(count);
      Console.WriteLine();

      Array.Resize(ref foodPil, (foodPil.Length) + 1);
      foodPil[countFoodKey] = menu;
      countFoodKey++;
    }

    private static void askOrderDrinkCounts(Drink[] menus, int index) {
      var menu = menus[index];
      int count;

      Console.Write("Masukkan jumlah yang ingin dibeli: ");
      count = Convert.ToInt32(Console.ReadLine());
      menu.setOrderCount(count);
      Console.WriteLine();

      Array.Resize(ref drinkPil, (drinkPil.Length) + 1);
      drinkPil[countDrinkKey] = menu;
      countDrinkKey++;
    }

    private static string equalBarrier() {
      return "------------------------";
    }
  }
}
