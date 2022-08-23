# Courses API

## Resources

`/courses`

### GET /courses?category=Dance

```json
{
"numberOfCourses": 0,
"data": []

}

```

### GET /courses/{id}

```json
{
    "id": "1", 
    "title": "Web APIs",
    "numberOfhours": 27.5,
    "deliveryLocation": "Online"
}
```

`deliveryLocation` = `Online` | `InPerson` | 