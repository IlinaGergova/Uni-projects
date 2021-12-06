package ProjectTask;

public enum Size {
    A1(1.0),A2(0.8),A3(0.6),A4(0.4),A5(0.3);

    private double price;
    private Size(double price){
        this.price = price;
    }

    public void setPrice(double price){
        this.price = price;
    }

    public double getPrice(){
        return price;
    }
}
