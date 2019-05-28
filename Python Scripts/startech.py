import requests,csv,os
from bs4 import BeautifulSoup





def loadLastPage(component):

    mainPage = requests.get("https://www.startech.com.bd/component/"+component)
    soup = BeautifulSoup(mainPage.content, 'html.parser')
    #pageList = soup.findAll(attrs={"class": "pagination"})
    #print(list(pageList))
    #print(pageList)

    numberOfPages = []
    for a in soup.select('.pagination li a'):
        numberOfPages.append(a.get_text())
    lastPage=numberOfPages[-2]
    return int(lastPage)


def loadData(component, lastPage):
    product_names = []
    product_links = []
    product_prices = []
    brand = []
    description=[]

    for i in range(1, lastPage + 1):
        number = i
        page = requests.get("https://www.startech.com.bd/component/" + component + "?page=" + str(number))
        soup = BeautifulSoup(page.content, 'html.parser')

        names = soup.findAll(attrs={"class": "product-name"})
        for i in range(0, len(names)):
            name = str(names[i].get_text()).replace("\n","")
            product_names.append(name)
            temp = str(names[i].get_text())
            tempList = temp.split(" ")
            temp = tempList[0].replace("\n","")
            print(temp)
            brand.append(temp)
            print(brand)
        for a in soup.select('.product-name a'):
            product_links.append(a['href'])

        prices = soup.findAll(attrs={"class": "price space-between"})
        for i in range(0, len(prices)):
            #print(prices[i].get_text())
            product_prices.append(prices[i].get_text()[1:-4])

    writeDataTofile(component,product_names,product_links,product_prices,brand)



def writeDataTofile(component,product,link,price,brand):
    if(os.path.exists('Startech-Final\\startech-'+component+'.csv')==False):
        wr = open('Startech-Final\\startech-'+component+'.csv', 'a', newline='')
        writer = csv.writer(wr)
        header = ['product_name', 'link', 'startech_rice', 'brand']
        writer.writerow(header)

        rows = zip(product,link,price,brand)


        for row in rows:
            writer.writerow(row)


def loadComponent(component):
    lastPage=loadLastPage(component)
    #print(lastPage)
    loadData(component,lastPage)

#loadComponent("motherboard")
#loadComponent("processor")
#loadComponent("ram")
loadComponent("hard-disk-drive")
