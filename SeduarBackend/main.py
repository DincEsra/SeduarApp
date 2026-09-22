from fastapi import FastAPI, Depends
from sqlalchemy.orm import Session
from pydantic import BaseModel
import database

# Tabloları oluştur
database.Base.metadata.create_all(bind=database.engine)

app = FastAPI()

# Veritabanı oturumunu yöneten yardımcı fonksiyon
def get_db():
    db = database.SessionLocal()
    try:
        yield db
    finally:
        db.close()

# Uygulamaya gelecek verinin şablonu (Pydantic)
class ProductCreate(BaseModel):
    name: str
    description: str
    price: float
    image_url: str
    category: str
    is_new_arrival: bool = False

@app.get("/")
def read_root():
    return {"mesaj": "SeduarApp Backend Başarıyla Çalışıyor!"}

# 1. API Kapısı: Yeni Ürün Ekleme (POST)
@app.post("/products/")
def create_product(product: ProductCreate, db: Session = Depends(get_db)):
    # Pydantic modelini SQLAlchemy veritabanı modeline çevir
    db_product = database.Product(**product.model_dump())
    db.add(db_product)
    db.commit()
    db.refresh(db_product)
    return db_product

# 2. API Kapısı: Tüm Ürünleri Listeleme (GET)
@app.get("/products/")
def get_products(db: Session = Depends(get_db)):
    return db.query(database.Product).all()

from fastapi import HTTPException # Dosyanın en üstündeki importlara HTTPException'ı eklemeyi unutma

# 3. API Kapısı: ID'ye Göre Ürün Silme (DELETE)
@app.delete("/products/{product_id}")
def delete_product(product_id: int, db: Session = Depends(get_db)):
    # Veritabanında bu ID'ye sahip ürünü arıyoruz
    product = db.query(database.Product).filter(database.Product.id == product_id).first()
    
    if not product:
        raise HTTPException(status_of_status=404, status_code=404, detail="Ürün bulunamadı")
    
    db.delete(product)
    db.commit()
    return {"mesaj": f"{product.id} numaralı ürün başarıyla silindi!"}