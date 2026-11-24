VERBOSE_BIN ?= verbose

RAYLIB_LIBS ?= -lraylib -lm
SHIM := csrc/verbose_raylib_shim.c
MOCK := csrc/headless_mock.c

.PHONY: all demos test test-real run-particles run-solar run-breakout clean

all: demos

demos:
	@mkdir -p build
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I src $(SHIM) $(RAYLIB_LIBS) --no-run -o build/01_particle_arena
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I src $(SHIM) $(RAYLIB_LIBS) --no-run -o build/02_solar_system_3d
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I src $(SHIM) $(RAYLIB_LIBS) --no-run -o build/03_arcade_breakout
	@echo "[OK] All 3 Raylib demos compiled against system libraylib -> build/"

run-particles:
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I src $(SHIM) $(RAYLIB_LIBS)

run-solar:
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I src $(SHIM) $(RAYLIB_LIBS)

run-breakout:
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I src $(SHIM) $(RAYLIB_LIBS)

# Headless CI/unit verification (works with no GPU / no display server)
test:
	$(VERBOSE_BIN) test tests/test_raylib_wrapper.vb -I src $(MOCK) -lm
	$(VERBOSE_BIN) demos/01_particle_arena.vb -I src $(MOCK) -lm
	$(VERBOSE_BIN) demos/02_solar_system_3d.vb -I src $(MOCK) -lm
	$(VERBOSE_BIN) demos/03_arcade_breakout.vb -I src $(MOCK) -lm

# Verify ABI struct sizes directly against installed system raylib.h / libraylib
test-real:
	$(VERBOSE_BIN) test tests/test_raylib_wrapper.vb -I src $(SHIM) $(RAYLIB_LIBS)

clean:
	rm -rf build demos/*.ll tests/*.ll
