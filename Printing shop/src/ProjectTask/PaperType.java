package ProjectTask;

public enum PaperType {
    PAPER(0.5),GLOSS(1.0),NEWSPAPER(0.4);
    private double price;
    private PaperType(double price){
        this.price = price;
    }

    public void setPrice(double price){
        this.price = price;
    }

    public double getPrice(){
        return price;
    }
}
