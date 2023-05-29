FROM node:18-alpine

WORKDIR /app

COPY . .

RUN npm ci

ENV PORT=5173

EXPOSE 5173

CMD ["npm", "run", "dev", "--", "--host", "0.0.0.0"]