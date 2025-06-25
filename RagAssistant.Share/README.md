# 🧠 RAG Assistant (.NET + Ollama + Qdrant)

ระบบ RAG (Retrieval-Augmented Generation) สำหรับค้นข้อมูลจากเอกสาร (PDF, Microsoft Word, Microsoft Excel) โดยดึงไฟล์จาก Microsoft OneDrive

พัฒนาโดยใช้ .NET Core + Ollama + Qdrant Vector DB แบบ local

---

## ✅ Requirements

| Tool | ใช้ทำอะไร | Download |
|------|-----------|----------|
| Ollama | Embedding Vector | [ollama.com](https://ollama.com) |
| Docker Desktop | รัน Qdrant Vector DB | [docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop) |

---

## ⚙️ Installation & Setup

### 1. ติดตั้ง Ollama ด้วย Docker

```bash
เปิด Docker Desktop

# โหลดและรัน Ollama Local บน Docker (ครั้งแรก)
docker run -d -p 11434:11434 --name ollama ollama/ollama

# ดาวน์โหลด embedding model (ทำครั้งแรก หรือเมื่อต้องการ update version) (ต้องเปิด Ollama ไว้)
docker exec -it ollama ollama pull nomic-embed-text

# ทดสอบ API (ต้องเปิด Ollama ไว้)
curl -X POST http://localhost:11434/api/embeddings -H "Content-Type: application/json" -d "{\"model\":\"nomic-embed-text\", \"prompt\": \"Hello World!\"}"
```

### 2. ติดตั้ง Qdrant ด้วย Docker

```bash
เปิด Docker Desktop

# โหลดและรัน Qdrant Vector DB บน Docker (ครั้งแรก)
docker run -d --name qdrant -p 6333:6333 -p 6334:6334 qdrant/qdrant

# ทดสอบ (ต้องเปิด Qdrant ไว้)
curl http://localhost:6333
```

## 🔥 n8n Integration

### 0. หา IP ของเครื่อง Local

```bash
== CMD ==
ipconfig

== n8n ==
แก้ IP ในทุกๆ *_base_url
// เพราะเดิมทีจะใช้ localhost แต่ใน n8n เมื่อใช้ localhost จะเป็น localhost ของ n8n เอง
```

### 1. API

```bash
== Program.cs ==
เพิ่มโค้ด app.Urls.Add("https://0.0.0.0:7274");
// เพื่อเปิดให้ IP ใดก็ได้เข้าถึง API ใน Port 7274 (HTTPS)
```
