package ProjectTask;

import java.io.*;
import java.util.ArrayList;
import java.util.List;

public class Main {

    public static void main(String[] args) {
        Machine m1 = new Machine(Color.BLACKANDWHITE, PaperType.PAPER, 20, 3, "m1");
        Machine m2 = new Machine(Color.BLACKANDWHITE, PaperType.PAPER, 30, 3, "m2");
        Machine m3 = new Machine(Color.COLORFUL, PaperType.PAPER, 25, 3, "m3");

        List <Machine> machines = new ArrayList <>();
        machines.add(m1);
        machines.add(m2);
        machines.add(m3);

        double salary = 200;
        Employee op1 = new Operator(salary);
        Employee man = new Manager(salary, 2);

        List <Employee> employees = new ArrayList <>();
        employees.add(op1);
        employees.add(man);

        PrintingShop ps = new PrintingShop(200, 10, employees, machines);

        Publication p1 = new Publication("Book", 15.5, "Bleach1", 33, Size.A5, PaperType.PAPER, Color.BLACKANDWHITE);
        Publication p2 = new Publication("Book", 15.5, "Bleach2", 43, Size.A5, PaperType.PAPER, Color.BLACKANDWHITE);
        Publication p3 = new Publication("Book", 15.5, "Bleach3", 83, Size.A5, PaperType.PAPER, Color.BLACKANDWHITE);
        Publication p4 = new Publication("Book", 15.5, "Bleach4", 23, Size.A5, PaperType.PAPER, Color.COLORFUL);
        Publication p5 = new Publication("Book", 15.5, "Bleach5", 33, Size.A5, PaperType.PAPER, Color.COLORFUL);

        List <Publication> pubs = new ArrayList <>();
        pubs.add(p1);
        pubs.add(p2);
        pubs.add(p3);
        pubs.add(p4);
        pubs.add(p5);

        ps.print(pubs);


        FileReader file = null;
        String fileName = "report.txt";
        try {
            file = new FileReader(fileName);
            int data;
            do {
                data = file.read();
                System.out.print((char) data);
            } while (data != -1);
        }
        catch (IOException e) {
            e.printStackTrace();
        }

    }
    }
