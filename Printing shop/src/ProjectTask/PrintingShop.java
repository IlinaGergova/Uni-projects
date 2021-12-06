package ProjectTask;

import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

public class PrintingShop {
    private double income;
    private double minIncome;
    private double outcome;
    private int minPublicationsForDiscount;
    private List<Employee> employees;
    private List<Machine> machines;
    private FileWriter file;

    public PrintingShop(double minIncome, int minPublicationsForDiscount, List<Employee> employees,
                        List<Machine> machines){
        this.minIncome = minIncome;
        this.employees = employees;
        this.machines = machines;
        this.minPublicationsForDiscount = minPublicationsForDiscount;
        income = 0;
        outcome = 0;
        createFile();
    }

    private void createFile(){
        try {
            file = new FileWriter("report.txt");
        }catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void calculateMoney(List<Publication> publications) {
        for (Publication p:publications) {
            income+=p.getPricePublication();
            outcome+=p.getPaperPrice();
        }
        if(publications.size() > minPublicationsForDiscount){
            for (Employee employee:employees) {
                if(employee instanceof Manager){
                    ((Manager) employee).increaseSalary();
                }
            }
        }
        for (Employee employee:employees) {
            outcome+= employee.getMainSalary();
        }
        try{
            file.write("Income "+this.income+'\n');
            file.write("Outcome "+this.outcome+'\n'+"--------------------------"+'\n');
            file.write("Publications information: "+'\n');
        }catch (IOException ioException){
            ioException.printStackTrace();
        }

    }

    public void print(List<Publication> publications){
        calculateMoney(publications);
        for (Publication p:publications) {
            int pages = p.getNumOfPages();
            while(pages != 0){
                for (Machine machine:machines) {
                    int pagesToBePrinted = 0;
                    if(!machine.runs() && machine.getColor().equals(p.getColor())){
                        if(pages > machine.getMaxPages()){
                            pagesToBePrinted = machine.getMaxPages();
                            pages-= machine.getMaxPages();
                        }
                        else{
                            pagesToBePrinted = pages;
                            pages = 0;
                        }
                    }
                    if(pagesToBePrinted!=0){
                        machine.beginProcess(p.getTitle(),pagesToBePrinted);
                    }
                }
            }
            try{
                file.write("- "+p.toString()+'\n');
            }
            catch (IOException ex){
                ex.printStackTrace();
            }

        }
        try {
            file.close();
        }
        catch (IOException ioException) {
            ioException.printStackTrace();
        }
    }
}
