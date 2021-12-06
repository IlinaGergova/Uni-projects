package ProjectTask;

import java.io.FileWriter;
import java.io.IOException;

public class Machine implements Runnable{
    private String machineId;
    private Color color;
    private PaperType paperType;
    private int maxPages;
    private int pagesPerMinute;
    private String currentPublication;
    private Thread thread;
    private int numOfPrintingPages;
    private int currentPaper;

    public Machine(Color color,PaperType paperType,int maxPages, int pagesPerMinute, String name){
        this.color = color;
        this.paperType = paperType;
        this.maxPages = maxPages;
        this.pagesPerMinute = pagesPerMinute;
        this.machineId = name;
        this.currentPaper = 200;
    }

    public void checkPaper() throws InsufficientNumberOfPaper {
        if (this.currentPaper<0){
            throw new InsufficientNumberOfPaper(maxPages);
        }
    }

    public void restockPaper(int pages){
        this.currentPaper+=pages;
    }

    public void beginProcess(String currentPublication, int numOfPagesToBePrinted) {
        this.currentPublication = currentPublication;
        this.numOfPrintingPages = numOfPagesToBePrinted;
        thread = new Thread(this, this.machineId);
        thread.start();
    }

    @Override
    public void run() {
        try {
            this.currentPaper-=this.numOfPrintingPages;
            checkPaper();

            System.out.println("[Machine "+this.machineId+"] is printing " +this.numOfPrintingPages+" pages from "+
             this.currentPublication);

            Thread.sleep(500);
        }
        catch (InterruptedException | InsufficientNumberOfPaper exception) {
            exception.printStackTrace();
        }
        finally {
            restockPaper(100);
        }
    }

    public boolean runs(){
        return thread!=null&&thread.isAlive();
    }

    public Color getColor() {
        return color;
    }

    public int getMaxPages() {
        return maxPages;
    }

    public int getPagesPerMinute() {
        return pagesPerMinute;
    }

    public void setPaperType(PaperType paperType){
        this.paperType = paperType;
    }
}
