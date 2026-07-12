# Stage 1: Build the application
FROM node:22-alpine AS build

# Set working directory
WORKDIR /app

# Copy package files and install dependencies
COPY src/Clients/frontend/package*.json ./
RUN npm ci

# Copy source code and build
COPY src/Clients/frontend .
RUN npm run build

# Stage 2: Serve with Nginx
FROM nginx:alpine

# Copy your custom Nginx configuration
COPY deploy/nginx/nginx.conf /etc/nginx/conf.d/default.conf

# Copy the built assets from Stage 1 to Nginx's public directory
COPY --from=build /app/dist /usr/share/nginx/html

# Expose port 80
EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]