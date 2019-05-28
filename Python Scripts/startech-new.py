import requests, csv, os
import pyodbc
from bs4 import BeautifulSoup



def load_last_page(component):
    main_page = requests.get("https://www.startech.com.bd/component/"+component)
    soup = BeautifulSoup(main_page.content, 'html.parser')
    page_list = soup.findAll(attrs={"class": "pagination"})
    #print(list(page_list))
    #print(pageList)

    number_of_pages = []
    for a in soup.select('.pagination li a'):
        number_of_pages.append(a.get_text())
    last_page = number_of_pages[-2]
    return int(last_page)


def load_data(component, last_page):
    product_names = []
    product_links = []
    product_prices = []
    product_models = []

    for i in range(1, last_page + 1):
        number = i
        page = requests.get("https://www.startech.com.bd/component/" + component + "?page=" + str(number))
        soup = BeautifulSoup(page.content, 'html.parser')
        # names = soup.findAll(attrs={"class": "product-name"})

        for a in soup.select('.product-name a'):
            product_names.append(a.get_text())
            product_links.append(a['href'])
            model_name = load_product_model(a['href'])
            product_models.append(model_name)

        prices = soup.findAll(attrs={"class": "price space-between"})
        for i in range(0, len(prices)):
            print(prices[i].get_text())
            product_prices.append(prices[i].get_text()[1:-4])
    write_data_to_file(component, product_names,product_models, product_links, product_prices)


def load_product_model(link):
    model ="No Model Found"
    page = requests.get(link)
    soup = BeautifulSoup(page.content, 'html.parser')
    try:
        model = soup.find('table', class_='data-table').find('td', text='Model').find_next_sibling('td').text
    except:
        pass
    print(model)
    return model


def write_data_to_file(component, product,model, link, price):
    if not os.path.exists('Startech-New-May\\startech-' + component + 'new.csv'):

        wr = open('Startech-New-May\\startech-'+component+'.csv', 'w', newline='')

        writer = csv.writer(wr)
        header = ['Product Name', 'Model', 'Link', 'Price']
        writer.writerow(header)

        rows = zip(product, model, link, price)
        for row in rows:
            writer.writerow(row)


def loadComponent(component):

    lastPage=load_last_page(component)
    #print(lastPage)
    load_data(component, lastPage)


if __name__ == '__main__':
    #load_product_models('processor')

    #loadComponent("motherboard")
    loadComponent("processor")
    #loadComponent("ram")
    #loadComponent("graphics-card")
