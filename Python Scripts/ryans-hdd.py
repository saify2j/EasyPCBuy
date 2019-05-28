from bs4 import BeautifulSoup
import requests
import csv
import os
from time import sleep
import pandas as pd


def get_last_page(url, component):
    main_page = requests.get(url + component+".html")
    soup = BeautifulSoup(main_page.content, 'html.parser')
    # print(soup.prettify())
    number_of_pages = []
    for a in soup.select('.pages li a'):
        number_of_pages.append(a.get_text())

    last_page = int(number_of_pages[-2])
    return last_page


def load_data(url, component):
    wr = open('Ryans-Final\\ryans-' + component + '2.csv', 'a', newline='', encoding="utf-8")
    writer = csv.writer(wr)
    #brands,names, models, links, prices, pic_links, storage, descriptions
    header = ['brand','name','model','link','price','pic_link','storage','description']
    writer.writerow(header)
    page = requests.get(url + component + ".html")
    soup = BeautifulSoup(page.content, 'html.parser')
    names = []
    links = []
    prices = []
    models = []
    storage = []
    descriptions = []
    brands = []
    pic_links = []

    for a in soup.select('.container h2 a'):
        links.append(a['href'])
        model_name, brand_name, description, cap = load_product_model(a['href'])
        brands.append(brand_name)

        names.append(a.get_text(strip=True))
        models.append(model_name)
        descriptions.append(description)
        storage.append(cap)
    price_list = soup.find_all('p', attrs={"class":"special-price"})
    for a in soup.find_all('a', attrs={"class": "product-image"}):
        if a.img:
            #print(a.img['src'])
            pic_links.append(a.img['src'])

    for j in price_list:
        prices.append(j.get_text()[18:])

    rows = zip(brands,names, models, links, prices, pic_links, storage, descriptions)

    for row in rows:
        writer.writerow(row)


print("Writing to file done...")


def load_product_model(link):
    model = "No Model Found"
    brand = "No Brand Specified"
    description = "No Description Found"
    capacity ="Storage not specified"
    try:
        page = requests.get(link)
        soup = BeautifulSoup(page.content, 'html.parser')
        model = soup.find('table', class_='data-table').find('th', text='Model').find_next_sibling('td').text
        brand = soup.find('table', class_='data-table').find('th', text='Brand').find_next_sibling('td').text
        capacity = soup.find('table', class_='data-table').find('th', text='Storage').find_next_sibling('td').text
        list_a = soup.find_all('div', attrs={"class": "std"})
        temp = str(list_a[1].get_text())
        temp = temp.strip()
        temp = temp.replace(",", "\n")
        description = temp
        # print(temp)

    except:
        pass
    return model, brand, description,capacity


def main():
    url = "https://ryanscomputers.com/storage/"
    # components = ["processor",
    #              "desktop-ram",
    #              "mainboard",
    #              "graphics-card"]
    components = ["internal-hdd"]
    for component in components:
        #last_page = get_last_page(url, component)
        load_data(url, component)


if __name__ == '__main__':
    main()
