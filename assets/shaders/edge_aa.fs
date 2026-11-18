#version 330

in vec2 fragTexCoord;
out vec4 finalColor;
uniform sampler2D texture0;
uniform vec2 texelSize;

float luminance(vec3 color) {
    return dot(color, vec3(0.299, 0.587, 0.114));
}

void main() {
    vec2 uv = fragTexCoord;
    vec3 center = texture(texture0, uv).rgb;
    float nw = luminance(texture(texture0, uv + texelSize*vec2(-1,-1)).rgb);
    float ne = luminance(texture(texture0, uv + texelSize*vec2(1,-1)).rgb);
    float sw = luminance(texture(texture0, uv + texelSize*vec2(-1,1)).rgb);
    float se = luminance(texture(texture0, uv + texelSize*vec2(1,1)).rgb);
    float low = min(luminance(center), min(min(nw,ne),min(sw,se)));
    float high = max(luminance(center), max(max(nw,ne),max(sw,se)));
    if (high-low < max(0.045, high*0.125)) {
        finalColor = vec4(center,1);
        return;
    }
    vec2 direction = vec2(sw+se-nw-ne, nw+sw-ne-se);
    float reduce = max((nw+ne+sw+se)/32.0, 1.0/128.0);
    direction = clamp(direction/(min(abs(direction.x),abs(direction.y))+reduce),
                      vec2(-6),vec2(6))*texelSize;
    vec3 inner = 0.5*(texture(texture0,uv-direction/6.0).rgb + texture(texture0,uv+direction/6.0).rgb);
    vec3 outer = 0.5*inner + 0.25*(texture(texture0,uv-direction*0.5).rgb + texture(texture0,uv+direction*0.5).rgb);
    float value = luminance(outer);
    finalColor = vec4(value < low || value > high ? inner : outer,1);
}
