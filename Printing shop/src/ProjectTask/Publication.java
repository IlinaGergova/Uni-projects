package ProjectTask;

public class Publication {
    private String typeOfPublication;
    private double pricePublication;
    private String title;
    private int numOfPages;
    private Size sizeOfPage;
    private PaperType paper;
    private double paperPrice;
    private Color color;

    public Publication(String typeOfPublication, double pricePublication, String title, int numOfPages,
                       Size sizeOfPage, PaperType paper, Color color){
        this.typeOfPublication = typeOfPublication;
        this.pricePublication = pricePublication;
        this.title = title;
        this.numOfPages = numOfPages;
        this.sizeOfPage = sizeOfPage;
        this.paper = paper;
        this.paperPrice = sizeOfPage.getPrice() + paper.getPrice();
        this.color = color;
    }

    public double getPaperPrice(){
        return paperPrice;
    }

    public double getPricePublication() {
        return pricePublication;
    }

    public Color getColor() {
        return color;
    }

    public int getNumOfPages() {
        return numOfPages;
    }

    public String getTitle() {
        return title;
    }

    public PaperType getPaper() {
        return paper;
    }

    @Override
    public String toString() {
        return "Publication{" +
                "typeOfPublication='" + typeOfPublication + '\'' +
                ", pricePublication=" + pricePublication +
                ", title='" + title + '\'' +
                ", numOfPages=" + numOfPages +
                ", sizeOfPage=" + sizeOfPage +
                ", paper=" + paper +
                ", paperPrice=" + paperPrice +
                ", color=" + color +
                '}';
    }
}
