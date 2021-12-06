package ProjectTask;

public class InsufficientNumberOfPaper extends Exception{
    private int pages;
    public InsufficientNumberOfPaper(int pages){
        this.pages = pages;
    }

    @Override
    public String toString() {
        return "InsufficientNumberOfPaper! The capacity of the printer is " + pages +
                " pages";
    }
}
