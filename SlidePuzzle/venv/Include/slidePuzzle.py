import wx
import random
from copy import copy, deepcopy

class Puzzle(wx.Frame):
    def __init__(self,*args,**kw):
        super(Puzzle,self).__init__(*args,**kw)
        self.initUI()
        
    def initUI(self):
        self.panel = wx.Panel(self)
        self.size = 0

        self.bitmap = wx.Bitmap('puzzle.png',wx.BITMAP_TYPE_PNG)
        resizedImg = self.bitmap.ConvertToImage()
        resizedImg = resizedImg.Scale(300, 300, wx.IMAGE_QUALITY_HIGH)
        resizedBitmap = resizedImg.ConvertToBitmap()
        self.img = wx.StaticBitmap(self.panel, 0, resizedBitmap, pos=(100, 90))
        
        self.introTitle = wx.StaticText(self.panel,label = 'Slide Puzzle',pos=(150,20))
        fontTitle = wx.Font(20, wx.DEFAULT, wx.NORMAL, wx.BOLD)
        self.introTitle.SetFont(fontTitle)

        self.font = wx.Font(10, wx.DEFAULT, wx.NORMAL, wx.BOLD)

        self.startBtn = wx.Button(self.panel,label='Start',pos=(200,420))
        self.startBtn.SetFont(self.font)
        self.startBtn.Bind(wx.EVT_BUTTON, self.chooseBoard)

        self.quitBtn = wx.Button(self.panel,label='Quit',pos=(200,460))
        self.quitBtn.Bind(wx.EVT_BUTTON,self.close)

        self.SetSize((550, 600))
        self.SetTitle('Slide puzzle game')
        self.Centre()
        self.Show(True)


    def close(self,e):
        self.Close(True)


    def chooseBoard(self,e):
        self.SetSize((400, 450))
        for child in self.panel.GetChildren():
            child.Show(False)
        self.threeByThree = wx.Button(self.panel, label='3 X 3', pos=(140, 80))
        self.threeByThree.Bind(wx.EVT_BUTTON, self.createBoard)
        self.threeByThree.SetSize((90,60))
        self.threeByThree.SetFont(self.font)

        self.fourByFour = wx.Button(self.panel, label='4 X 4', pos=(140, 160))
        self.fourByFour.Bind(wx.EVT_BUTTON, self.createBoard)
        self.fourByFour.SetSize((90, 60))
        self.fourByFour.SetFont(self.font)

        self.fiveByFive = wx.Button(self.panel, label='5 X 5', pos=(140, 240))
        self.fiveByFive.Bind(wx.EVT_BUTTON, self.createBoard)
        self.fiveByFive.SetSize((90, 60))
        self.fiveByFive.SetFont(self.font)


    def createBoard(self,e):
        for child in self.panel.GetChildren():
            child.Show(False)

        l = e.GetId()
        if l == self.threeByThree.GetId():
            self.size = 3
            self.SetSize((450, 470))
        elif l == self.fourByFour.GetId():
            self.size = 4
            self.SetSize((500, 520))
        elif l == self.fiveByFive.GetId():
            self.size = 5
            self.SetSize((550, 570))

        self.backBtn = wx.Button(self.panel,label='Back',pos=(20,20))
        self.backBtn.Bind(wx.EVT_BUTTON,self.chooseBoard)

        self.shuffledMatrixOfBtns = []
        self.h = 80
        number = self.size*self.size
        self.arrOfOrdredNumbers = []
        self.arrOfOrdredNumbersCopy = []
        for i in range(number):
            self.arrOfOrdredNumbers.append(i)

        for i in range(0,self.size):
            self.btnsLine = []
            self.h+=60
            self.w = 100
            for j in range(0,self.size):
                self.w += 60
                
                tempNum = random.choice(self.arrOfOrdredNumbers)
                if tempNum == 0:
                    self.currentBtn = wx.Button(self.panel, label="", pos=(self.w, self.h))
                    self.arrOfOrdredNumbersCopy.append("")
                    self.currentBtn.SetBackgroundColour((153, 206, 255))
                else:
                    self.currentBtn = wx.Button(self.panel, label=str(tempNum), pos=(self.w, self.h))
                    self.arrOfOrdredNumbersCopy.append(str(tempNum))
                    self.currentBtn.SetBackgroundColour((204, 230, 255))

                self.arrOfOrdredNumbers.remove(tempNum)

                self.currentBtn.SetFont(self.font)
                self.currentBtn.SetSize((60, 60))
                self.currentBtn.Bind(wx.EVT_BUTTON,self.move)
                self.btnsLine.append(self.currentBtn)
                
            self.shuffledMatrixOfBtns.append(self.btnsLine)
        
        self.resetBtn = wx.Button(self.panel,label='Reset',pos=(20,60))
        self.resetBtn.Bind(wx.EVT_BUTTON,self.reset)

        self.newGameBtn = wx.Button(self.panel, label='New Game', pos=(20, 100))
        self.newGameBtn.Bind(wx.EVT_BUTTON, self.newGame)
        
    def move(self,e):
        current = e.GetId()
        for i in range(self.size):
            for j in range(self.size):
                if self.shuffledMatrixOfBtns[i][j].GetId() == current:
                    currentLabel = self.shuffledMatrixOfBtns[i][j].GetLabel()

                    if i != 0:
                        if self.shuffledMatrixOfBtns[i - 1][j].GetLabel() == "":
                            self.shuffledMatrixOfBtns[i - 1][j].SetLabel(currentLabel)
                            self.shuffledMatrixOfBtns[i - 1][j].SetBackgroundColour((204, 230, 255))
                            self.shuffledMatrixOfBtns[i][j].SetLabel("")
                            self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((153, 206, 255))
                            self.checkForWin()
                        
                    if i != self.size-1:
                        if self.shuffledMatrixOfBtns[i + 1][j].GetLabel() == "":
                            self.shuffledMatrixOfBtns[i + 1][j].SetLabel(currentLabel)
                            self.shuffledMatrixOfBtns[i + 1][j].SetBackgroundColour((204, 230, 255))
                            self.shuffledMatrixOfBtns[i][j].SetLabel("")
                            self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((153, 206, 255))
                            self.checkForWin()

                    if j != 0:
                        if self.shuffledMatrixOfBtns[i][j - 1].GetLabel() == "":
                            self.shuffledMatrixOfBtns[i][j - 1].SetLabel(currentLabel)
                            self.shuffledMatrixOfBtns[i][j - 1].SetBackgroundColour((204, 230, 255))
                            self.shuffledMatrixOfBtns[i][j].SetLabel("")
                            self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((153, 206, 255))
                            self.checkForWin()
                    
                    if j != self.size-1:
                        if self.shuffledMatrixOfBtns[i][j + 1].GetLabel() == "":
                            self.shuffledMatrixOfBtns[i][j + 1].SetLabel(currentLabel)
                            self.shuffledMatrixOfBtns[i][j + 1].SetBackgroundColour((204, 230, 255))
                            self.shuffledMatrixOfBtns[i][j].SetLabel("")
                            self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((153, 206, 255))
                            self.checkForWin()


    def reset(self,e):
        ind = -1
        for i in range(self.size):
            for j in range(self.size):
                ind += 1
                firstGeneratedValue = self.arrOfOrdredNumbersCopy[ind]
                if firstGeneratedValue ==  "":
                    self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((153, 206, 255))
                else:
                    self.shuffledMatrixOfBtns[i][j].SetBackgroundColour((204, 230, 255))
                self.shuffledMatrixOfBtns[i][j].SetLabel(firstGeneratedValue)


    def newGame(self,e):
        self.createBoard(e)

    def checkForWin(self):
        counter = 0
        if self.shuffledMatrixOfBtns[self.size-1][self.size-1].GetLabel() != "":
            return
        for i in range(self.size):
            for j in range(self.size):
                counter += 1
                if i != self.size-1 and j != self.size-1:
                    if self.shuffledMatrixOfBtns[i][j].GetLabel() != str(counter):
                        return


                
        self.winText = wx.StaticText(self.panel,label="You won!",pos=(200,50))
        self.winText.SetFont(self.font)
        self.disableBtns()

    def disableBtns(self):
        self.resetBtn.Disable()
        for i in range(self.size):
            for j in range(self.size):
                self.shuffledMatrixOfBtns[i][j].Disable()


def main():
    puzzle = wx.App()
    Puzzle(None)
    puzzle.MainLoop()

main()